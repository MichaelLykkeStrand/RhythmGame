using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameObject player;
    private bool isVisited = false;

    public bool IsVisited { get => isVisited; set => isVisited = value; }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Teleport()
    {
        player.transform.position = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isVisited == false)
        {
            this.IsVisited = true;
            //player.GetComponent<Player>().CurrentCheckpoint = this;
        }
    }
}
