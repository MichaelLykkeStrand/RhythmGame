using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongRepository : MonoBehaviour, ISongRepository 
{
    public static ISongRepository instance;
    private List<Song> songList = new List<Song>();

    public Song GetSong()
    {
        throw new System.NotImplementedException();
    }

    public List<Song> GetSongs()
    {
        return songList;
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
        var info = new DirectoryInfo(Application.streamingAssetsPath);
        var songDirInfo = info.GetDirectories();
        foreach (var dirInfo in songDirInfo)
        {
            var fileInfo = dirInfo.GetFiles();
            foreach (FileInfo file in fileInfo)
            {
                if (file.Extension == ".song")
                {
                    Song tempSong = Serializer.DeSerializeObject<Song>(file.FullName);
                    songList.Add(tempSong);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
