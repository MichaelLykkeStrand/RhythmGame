using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongRepository : MonoBehaviour, ISongRepository 
{
    public Song GetSong()
    {
        throw new System.NotImplementedException();
    }

    public List<Song> GetSongs()
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo(Application.streamingAssetsPath);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".song")
            {
                Song song = Serializer.DeSerializeObject<Song>(file.FullName);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
