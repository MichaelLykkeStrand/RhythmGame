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
    private PlayerBunnyMovement playerMovement;
    int inputIndex = 0;

    public bool IsRunning { get => isRunning; set => isRunning = value; }

    // Start is called before the first frame update
    void Start()
    {
        SetNodeOrder();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBunnyMovement>();
    }

    private void SetNodeOrder()
    {
        nodes = GetComponentsInChildren<PositionNode>();
        for (int i = 0; i < nodes.Length-1; i++)
        {
            nodes[i].NextNode = nodes[i + 1];
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
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = GameController.Instance.marginOfErrorInSeconds;
            double audioTime = GameController.GetAudioSourceTime() - (GameController.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetButtonDown(nodes[inputIndex & nodes.Length-1].Input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    print($"Hit on {inputIndex} note");
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
            
        }
    }

    private void Hit()
    {
        playerMovement.Move();
        ScoreController.Instance.Hit();

    }
    private void Miss()
    {
        playerMovement.Move();
        ScoreController.Instance.Miss();
    }

}
