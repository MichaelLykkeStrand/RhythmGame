using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBunnyMovement : PlayerMovement
{
    [SerializeField] private PositionNode currentNode;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        PositionNode nextNode = currentNode.GetNextNode;
        Vector2 nextPos = nextNode.transform.position;
        nextPos.y += player.Height / 2;
        float jumpPower = currentNode.JumpPower;
        float transitionTime = currentNode.TransitionTime;

        transform.DOJump(nextPos,jumpPower,1,transitionTime);
        this.currentNode = nextNode;

    }
}
