using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        System.Random random = new System.Random();
        int number = random.Next(3);
        anim.SetInteger("Grow", number);
    }
}
