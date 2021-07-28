using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name;
    public string Description;
    private Sprite sprite; //For later use?
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Item has been picked up");
        Player player;
        try
        {
            player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                Pickup();
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    void Pickup()
    {
        ScoreController.Instance.Hit(50);
        spriteRenderer.enabled = false;
        particleSystem.Play();
    }


}
