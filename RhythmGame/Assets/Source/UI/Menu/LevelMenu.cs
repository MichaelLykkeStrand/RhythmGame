using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject content;
    public GameObject buttonPrefab;
    private ISongRepository songRepository;
    // Start is called before the first frame update
    void Start()
    {
        songRepository = SongRepository.instance;
        List<Song> songs = songRepository.GetSongs();
        foreach (var song in songs)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
