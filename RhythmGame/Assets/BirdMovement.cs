using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour, IExplosion
{
    [SerializeField] float speed = 2f;
    [SerializeField] int color = 0;
    private Animator animator;

    public void Explode()
    {
        Debug.Log("Bird should explode!");
        animator.SetBool("Explode", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Color", color);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            BirbExploder.instance.AddBird(gameObject);
            transform.Translate(Vector2.left * Time.deltaTime * speed, Space.World);
        }
        else
        {
            BirbExploder.instance.RemoveBird(gameObject);
        }
    }
}
