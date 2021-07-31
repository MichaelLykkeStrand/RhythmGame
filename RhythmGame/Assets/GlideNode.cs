using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideNode : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private GameObject growthBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I was called");
        Player player;
        try
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                growthBlock.SetActive(true);
               
            }
        }
        catch (System.Exception)
        {

        }
        plant.Grow();
    }
}
