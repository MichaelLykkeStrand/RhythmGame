using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour, IWindow
{

    public static EndMenuController instance;
    [SerializeField] GameObject noCarrot;
    [SerializeField] GameObject bronzeCarrot;
    [SerializeField] GameObject silverCarrot;
    [SerializeField] GameObject goldenCarrot;
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
        int _score = ScoreController.Instance.GetScore();
        Song song = GameController.Instance.GetCurrentSong();
        score.text = _score.ToString();
        if(_score > song.gold)
        {
            goldenCarrot.SetActive(true);
            return;
        }else if(_score < song.gold && _score > song.silver)
        {
            silverCarrot.SetActive(true);
            return;
        }
        else if(_score > song.bronze)
        {
            bronzeCarrot.SetActive(true);
        }
        else
        {
            noCarrot.SetActive(true);
        }
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
