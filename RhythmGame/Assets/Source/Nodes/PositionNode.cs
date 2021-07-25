using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PositionNode : MonoBehaviour
{
    [System.Serializable]
    public enum InputEnum // your custom enumeration
    {
        none,
        up,
        down,
        left,
        right
    };

    [SerializeField] private PositionNode nextNode;
    [SerializeField] private float jumpPower;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private bool isWall = false;
    private double visitTime;
    public int index;
    public bool isCheckpoint = false;
    public InputEnum input = InputEnum.none;

    public PositionNode NextNode { get => nextNode; set => nextNode = value; }
    public float JumpPower { get => jumpPower;}
    public float TransitionTime { get => transitionTime; set => transitionTime = value; }
    public string GetInput()
    {
        if (input == InputEnum.none) return "";
        else if (input == InputEnum.up) return "Up";
        else if (input == InputEnum.down) return "Down";
        else if (input == InputEnum.left) return "Left";
        else if (input == InputEnum.right) return "Right";
        return "";
    }

    public bool IsWall { get => isWall; set => isWall = value; }
    public double VisitTime { get => visitTime; set => visitTime = value; }
}
