using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbExploder : MonoBehaviour
{

    private List<GameObject> birds;
    public static BirbExploder instance;
    System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        GameController.EventBus.Subscribe<NodeHitEvent>(OnNodeHit);
        DontDestroyOnLoad(gameObject);
        instance = this;
        birds = new List<GameObject>();
        random = new System.Random();
    }

    void OnNodeHit(NodeHitEvent nodeHitEvent)
    {
        Explode();
    }

    public void Explode()
    {
        Debug.Log("Explode!");
        foreach (var bird in birds)
        {
            try
            {
                int value = random.Next(3);
                if(value == 0)
                {
                    var explosion = bird.GetComponent<IExplosion>();
                    explosion.Explode();
                }

            }
            catch (System.Exception)
            {
            }
            //Select random bird
        }
    }

    public void AddBird(GameObject bird)
    {
        //Debug.Log("Bird added");
        if (birds.Contains(bird)) return;
        else birds.Add(bird);
    }

    public void RemoveBird(GameObject bird)
    {
        if (birds.Contains(bird)) birds.Remove(bird);
    }
}
