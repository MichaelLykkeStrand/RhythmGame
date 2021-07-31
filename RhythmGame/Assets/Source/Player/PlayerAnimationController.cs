using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Vector3 prevPos, velocity;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        prevPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        velocity = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
        float xDeadzone = 0.1f;
        if (velocity.x < -xDeadzone)
        {
            spriteRenderer.flipX = true;
        }
        else if(velocity.x > xDeadzone)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("VelocityY", velocity.y);
    }
}
