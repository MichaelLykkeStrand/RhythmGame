using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour, IWindow
{
    public GameObject content;
    public GameObject buttonPrefab;
    [SerializeField]private GameObject exitPrompt;
    private ISongRepository songRepository;
    // Start is called before the first frame update
    void Start()
    {
        songRepository = SongRepository.instance;
        List<Song> songs = songRepository.GetSongs();
        foreach (var song in songs)
        {
            var buttonInstance = Instantiate(buttonPrefab);
            var controller = buttonInstance.GetComponent<LevelButtonController>();
            controller.songNameText.text = song.songName;

            buttonInstance.transform.SetParent(content.transform);

            var button = buttonInstance.GetComponent<Button>();
            button.onClick.AddListener(()=> OnLevelButtonClicked(song));
        }
        TransitionController.SetLoading(false);
    }

    private void OnLevelButtonClicked(Song song)
    {
        TransitionController.SetLoading(true);
        GameController.Instance.StartSong(song);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        exitPrompt.SetActive(true);
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }

    public void Close()
    {
        throw new System.NotImplementedException();
    }
}
