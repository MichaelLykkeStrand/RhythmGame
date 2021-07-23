using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rigidbody2D;
    private bool canJump = true;

    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void Jump()
    {
        if (canJump && Input.GetButtonDown("Jump"))
        {
            rigidbody2D.AddForce(transform.up* jumpForce, ForceMode2D.Impulse);
        }
    }
}
