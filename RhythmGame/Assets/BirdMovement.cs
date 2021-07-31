using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed, Space.World);
        }

    }
}
