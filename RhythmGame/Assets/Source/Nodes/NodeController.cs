using System.Collections;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeController : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    private bool isRunning = false;
    private List<double> timeStamps = new List<double>();
    private PositionNode[] nodes;
    private PositionNode currentCheckpoint;
    private PlayerBunnyMovement playerMovement;
    PositionNode currentNode;

    int inputIndex = 0;
    private bool doingLongNote = false;
    [SerializeField] private float nodeAnimSpeed = 1f;

    public bool IsRunning { get => isRunning; set => isRunning = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBunnyMovement>();
        SetNodeOrder();
    }

    private void SetNodeOrder()
    {
        nodes = GetComponentsInChildren<PositionNode>();
        playerMovement.transform.position = nodes[0].transform.position+ new Vector3(0,0.5f,0);
        currentCheckpoint = nodes[0];
        currentCheckpoint.index = 0;
        playerMovement.currentNode = nodes[0];
        for (int i = 0; i < nodes.Length-1; i++)
        {
            try { nodes[i].PrevNode = nodes[i - 1]; } catch (Exception) { }
            try { nodes[i].NextNode = nodes[i + 1]; } catch (Exception) { }
            nodes[i].index = i;
        }
    }

    public void SetTimeStamps(Note[] notes)
    {
        for (int i = 0; i < notes.Length; i++)
        {
            Note note = notes[i];
            
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, GameController.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                try
                {
                    PositionNode node = nodes[i];
                    node.activationTime = (metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + metricTimeSpan.Milliseconds / 1000f) - nodeAnimSpeed;
                    node.assignedTime = (metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + metricTimeSpan.Milliseconds / 1000f);
                }
                catch (Exception){}
            }
        }
    }

    private void FixedUpdate()
    {
        if (doingLongNote)
        {
            double timeStamp = timeStamps[inputIndex];
            double audioTime = GameController.Instance.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);
            Debug.Log("Doing long note!");
            if (Input.GetButton(currentNode.PrevNode.GetInput()))
            {
                ScoreController.Instance.Hit(1);
            }
            else
            {
                doingLongNote = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        double timeStamp = timeStamps[inputIndex];
        double marginOfError = GameController.Instance.marginOfErrorInSeconds;
        double audioTime = GameController.Instance.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);
        currentNode = nodes[inputIndex];
        string key = currentNode.GetInput();

        if (inputIndex < timeStamps.Count)
        {
            //Handle hit
            try
            {
                if (Input.GetButtonDown(key))
                {
                    if (Math.Abs(audioTime - timeStamp) < marginOfError)
                    {
                        float accuracy = (float)Math.Abs(audioTime - timeStamp);
                        if (currentNode.IsLongNode)
                        {
                            doingLongNote = true;
                            currentNode.Hit(accuracy);
                        }
                        else
                        {
                            currentNode.Hit(accuracy);
                            print($"Hit on {inputIndex} note");
                        }
                    }
                    else
                    {
                        print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    }
                }
            }
            catch (Exception){}

            if (timeStamp <= audioTime)
            {
                NodePassed(currentNode, timeStamp);
                Debug.Log($"Autojump");
                playerMovement.Move();
            }

            //Handle miss
            if (timeStamps[inputIndex-1] + marginOfError <= audioTime)
            {
                PositionNode node = currentNode.PrevNode;
                if (node.GetInput()!= "" && !node.IsLongNode && node.isHit == false)
                {
                    node.Miss();
                    node.isHit = true;
                    print($"Missed {key} key");
                }
            }
        }
    }

    private void NodePassed(PositionNode node, double time)
    {
        node.VisitTime = time;
        if (node.isCheckpoint)
        {
            this.currentCheckpoint = node;
        }
        inputIndex++;
    }

    public void GoToCheckpoint()
    {
        GameController.Instance.SetAudioTime((float)currentCheckpoint.VisitTime);
        inputIndex = currentCheckpoint.index;
        playerMovement.transform.position = currentCheckpoint.transform.position;
        playerMovement.currentNode = currentCheckpoint;
    }

    void OnGUI()
    {
        if (Application.isEditor)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Checkpoint: "+currentCheckpoint.index);
            var audioTime = GameController.Instance.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);
            GUI.Label(new Rect(10, 30, 100, 20), "AudioTime: " + audioTime);
            GUI.Label(new Rect(10, 90, 100, 20), "InputIndex: " + inputIndex);
        }
    }

}
