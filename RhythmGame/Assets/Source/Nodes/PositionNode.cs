using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionNode : MonoBehaviour
{
    [SerializeField] private PositionNode nextNode;
    [SerializeField] private float jumpPower;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private bool isWall = false;
    [SerializeField] private string input;


    public PositionNode NextNode { get => nextNode; set => nextNode = value; }
    public float JumpPower { get => jumpPower;}
    public float TransitionTime { get => transitionTime; set => transitionTime = value; }
    public string Input { get => input; set => input = value; }
    public bool IsWall { get => isWall; set => isWall = value; }
}
