using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private AudioSource audioSource;
    public float songDelayInSeconds;
    public NodeController nodeController;
    public static MidiFile midiFile;
    public int inputDelayInMilliseconds;

    public float marginOfErrorInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && nodeController.IsRunning == true)
        {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && nodeController.IsRunning == false)
        {
            ResumeGame();
        }
        
    }

    public void StartSong(Song song)
    {

        //Load premade scene
        if(song.associatedScene.Length > 0)
        {
            StartCoroutine(LoadAsyncScene(song));
        }
        else
        {
            throw new NotImplementedException();
        }
       
    }

    IEnumerator LoadAsyncScene(Song song)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(song.associatedScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        nodeController = GameObject.FindGameObjectWithTag("NodeController").GetComponent<NodeController>();
        ReadFromFile(song);
        
    }

    async private void ReadFromFile(Song song)
    {
        Debug.Log("Loading song!");
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + song.songName + "/" + song.songName + ".mid");
        var clipPath = Application.streamingAssetsPath + "/" + song.songName + "/" + song.songName + ".wav";
        AudioClip audioClip = await LoadClip(clipPath);
        audioSource.clip = audioClip;
        Debug.Log("Finished loading song!");
        TransitionController.SetLoading(false);
        StartGame();
    }

    async Task<AudioClip> LoadClip(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            uwr.SendWebRequest();

            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!uwr.isDone) await Task.Delay(5);

                if (uwr.isNetworkError || uwr.isHttpError) Debug.Log($"{uwr.error}");
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }
        return clip;
    }

    public void StartAudio()
    {
        Instance.audioSource.Play();
    }

    public void SetAudioTime(float time)
    {
        Instance.audioSource.time = time;
    }


    public double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.time;
    }

    public void StartGame()
    {
        Debug.Log("Starting game!");
        var notes = midiFile.GetNotes();
        var array = new Note[midiFile.GetNotes().Count];
        notes.CopyTo(array, 0);
        nodeController.SetTimeStamps(array);
        ResumeGame();

        Invoke(nameof(StartAudio), songDelayInSeconds);
    }


    public void PauseGame()
    {
        nodeController.IsRunning = false;
        audioSource.Pause();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        nodeController.IsRunning = true;
        audioSource.UnPause();
        Time.timeScale = 1;
    }

}
