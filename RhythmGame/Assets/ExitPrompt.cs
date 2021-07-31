using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPrompt : MonoBehaviour, IWindow
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Exit()
    {

        Application.Quit();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
