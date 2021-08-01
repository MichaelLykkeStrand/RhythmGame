using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongRepository : MonoBehaviour, ISongRepository 
{
    public static ISongRepository instance;


    public Song GetSong(string keyword)
    {
        List<Song> songList = GetSongs();
        foreach (var song in songList)
        {
            if (song.songName.Contains(keyword)) return song;
        }
        return null;
    }

    public List<Song> GetSongs()
    {
        List<Song> songList = new List<Song>();
        var info = new DirectoryInfo(Application.streamingAssetsPath);
        var songDirInfo = info.GetDirectories();
        foreach (var dirInfo in songDirInfo)
        {
            var fileInfo = dirInfo.GetFiles();
            foreach (FileInfo file in fileInfo)
            {
                if (file.Extension == SongUtils.FILE_EXTENSION)
                {
                    Song tempSong = Serializer.DeSerializeObject<Song>(file.FullName);
                    songList.Add(tempSong);
                }
            }
        }
        return songList;
    }

    public List<Song> GetSongs(string keyword)
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        Awake();
    }

    // Start is called before the first frame update
    void Awake()
    {
        /*
        Song song = new Song();
        song.author = "author name";
        song.difficulty = 5;
        song.associatedScene = "scene name";
        song.songName = "song name";
        Serializer.SerializeObject<Song>(song, Application.streamingAssetsPath+"/song.generated");
        */


        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
