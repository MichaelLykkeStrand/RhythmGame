using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

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
    [SerializeField] private PositionNode prevNode;
    [SerializeField] private float jumpPower;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private bool isWall = false;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject ghostBlock;
    [SerializeField] private float animationTime;
    private float t;
    private Vector3 ghostBlockSpawnpoint;
    private Vector3 blockSpawnpoint;
    public float activationTime;
    public float assignedTime;
    [SerializeField] private double visitTime;
    public int index;
    public bool isCheckpoint = false;
    public InputEnum input = InputEnum.none;

    public PositionNode NextNode { get => nextNode; set => nextNode = value; }
    public float JumpPower { get => jumpPower; }
    public float TransitionTime { get => transitionTime; set => transitionTime = value; }
    public bool IsWall { get => isWall; set => isWall = value; }
    public double VisitTime { get => visitTime; set => visitTime = value; }
    public PositionNode PrevNode { get => prevNode; set => prevNode = value; }

    public string GetInput()
    {
        if (input == InputEnum.none) return "";
        else if (input == InputEnum.up) return "Up";
        else if (input == InputEnum.down) return "Down";
        else if (input == InputEnum.left) return "Left";
        else if (input == InputEnum.right) return "Right";
        return "";
    }

    void Start()
    {
        block.GetComponent<SpriteRenderer>().enabled = false;
        ghostBlock.GetComponent<SpriteRenderer>().enabled = true;

        blockSpawnpoint = new Vector3(block.transform.position.x, block.transform.position.y);
        ghostBlockSpawnpoint = new Vector3(ghostBlock.transform.position.x, ghostBlock.transform.position.y);

        //Position blocks
        ghostBlock.transform.position = block.transform.position;
        block.transform.position = ghostBlockSpawnpoint;

    }

    private float blockPopInOffset = 0.6f;
    void Update()
    {
        //TODO growth animation stuff
        if (prevNode == null) {
            block.transform.position = blockSpawnpoint;
            block.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }

        if (GameController.Instance.GetAudioSourceTime() > prevNode.activationTime)
        {
            block.transform.DOMove(blockSpawnpoint, prevNode.assignedTime - prevNode.activationTime);
            block.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (GameController.Instance.GetAudioSourceTime() - activationTime > 0)
        {

        }

        //TODO block animation stuff
    }
}
