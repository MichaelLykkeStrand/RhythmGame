using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbExploder : MonoBehaviour
{

    private List<GameObject> birds;
    public static BirbExploder instance;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        birds = new List<GameObject>();
    }


    public void Explode()
    {
        Debug.Log("Explode!");
        foreach (var bird in birds)
        {
            try
            {
                System.Random random = new System.Random();
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
