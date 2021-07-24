using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISongRepository
{
    Song GetSong();
    List<Song> GetSongs();
    void Reload();
}
