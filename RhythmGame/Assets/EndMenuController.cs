using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour, IWindow
{

    public static EndMenuController instance;
    [SerializeField] Text score;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {

         DestroyAllDontDestroyOnLoadObjects();
         SceneManager.LoadScene("LevelBrowser");

    }


    public void Open()
    {
        gameObject.SetActive(true);
        score.text = ScoreController.Instance.GetScore().ToString();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    private void DestroyAllDontDestroyOnLoadObjects()
    {
        var go = new GameObject();
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);
    }
}
