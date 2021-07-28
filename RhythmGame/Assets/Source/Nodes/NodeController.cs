using System.Collections;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;
using System;
using static PositionNode;

public class NodeController : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    private bool isRunning = false;
    private List<double> timeStamps = new List<double>();
    private PositionNode[] nodes;
    private PositionNode currentCheckpoint;
    private PlayerBunnyMovement playerMovement;
    int inputIndex = 0;
    private bool doingLongNote = false;

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
                    node.activationTime = (metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + metricTimeSpan.Milliseconds / 1000f) - 0.5f;
                    node.assignedTime = (metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + metricTimeSpan.Milliseconds / 1000f);
                }
                catch (Exception){}
            }
        }
    }
    PositionNode currentNode;
    private void FixedUpdate()
    {

        if (doingLongNote)
        {
            double timeStamp = timeStamps[inputIndex];
            double audioTime = GameController.Instance.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);
            Debug.Log("Doing long note!");
            if (Input.GetButton(currentNode.PrevNode.GetInput()) && (audioTime - timeStamp) < 0)
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
                    if (Math.Abs(audioTime - timeStamp) < marginOfError) //Redo
                    {
                        if(currentNode.IsLongNode)
                        {
                            CinemachineEffects.instance.Punch();
                            doingLongNote = true;
                            ScoreController.Instance.Hit();
                        }
                        else
                        {
                            NodePassed(currentNode, timeStamp);
                            Hit(currentNode);
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
            //Handle miss
            if (timeStamp + marginOfError <= audioTime)
            {
                
                if (key!= "")
                {
                    NodePassed(currentNode, timeStamp);
                    Miss(currentNode);
                    print($"Missed {key} key");

                }
                else
                {
                    NodePassed(currentNode, timeStamp);
                    Debug.Log($"Autojump");
                    playerMovement.Move();

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

    private void Hit(PositionNode node)
    {
        playerMovement.Move();
        ScoreController.Instance.Hit();
        node.Hit();
        CinemachineEffects.instance.Punch();

    }
    private void Miss(PositionNode node)
    {
        playerMovement.Move();
        ScoreController.Instance.Miss();    
        
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
        if (Application.isEditor)  // or check the app debug flag
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Checkpoint: "+currentCheckpoint.index);
            var audioTime = GameController.Instance.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);
            GUI.Label(new Rect(10, 30, 100, 20), "AudioTime: " + audioTime);
            GUI.Label(new Rect(10, 90, 100, 20), "InputIndex: " + inputIndex);
        }
    }

}
