using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBunnyMovement : PlayerMovement
{
    public PositionNode currentNode;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Move()
    {
        Jump();
    }

    private void Jump()
    {
        PositionNode nextNode = currentNode.NextNode;
        Vector2 nextPos = nextNode.transform.position;
        nextPos.y += player.Height / 2;
        float jumpPower = currentNode.JumpPower;
        float transitionTime = currentNode.TransitionTime;

        transform.DOJump(nextPos,jumpPower,1,transitionTime);
        this.currentNode = nextNode;

    }

    void OnGUI()
    {
        if (Application.isEditor)  // or check the app debug flag
        {
            GUI.Label(new Rect(10, 50, 100, 20), "CurrentNode: " + currentNode.index);
            GUI.Label(new Rect(10, 70, 100, 20), "NextNode: " + currentNode.NextNode.index);
        }
    }
}
