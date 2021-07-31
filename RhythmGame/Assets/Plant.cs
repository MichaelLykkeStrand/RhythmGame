using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IGrowable
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        anim = GetComponent<Animator>();
        System.Random random = new System.Random();
        int number = random.Next(3);
        anim.SetInteger("Grow", number);
    }
}
