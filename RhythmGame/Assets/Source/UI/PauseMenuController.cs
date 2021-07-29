using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    GameController gameController = GameController.Instance;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameController.IsRunning())
        {
            gameController.PauseGame();
            canvas.enabled = true;
            canvas.referencePixelsPerUnit = 101;
            canvas.referencePixelsPerUnit = 100;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !gameController.IsRunning())
        {
            canvas.enabled = false;
            gameController.ResumeGame();
        }
    }

    public void OnExitButtonClick()
    {
        DestroyAllDontDestroyOnLoadObjects();
        SceneManager.LoadScene("LevelBrowser");
    }

    public void OnResumeButtonClick()
    {
        canvas.enabled = false;
        
        gameController.ResumeGame();
    }

    private void DestroyAllDontDestroyOnLoadObjects()
    {
        var go = new GameObject();
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);
    }
}
