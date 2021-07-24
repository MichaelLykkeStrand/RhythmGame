﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionNode : MonoBehaviour
{
    [SerializeField] private PositionNode nextNode;
    [SerializeField] private float jumpPower;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private KeyCode input;


    public PositionNode NextNode { get => nextNode; set => nextNode = value; }
    public float JumpPower { get => jumpPower;}
    public float TransitionTime { get => transitionTime; set => transitionTime = value; }
    public KeyCode Input { get => input; set => input = value; }
}