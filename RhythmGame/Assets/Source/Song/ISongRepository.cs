using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISongRepository
{
    Song GetSong(string keyword);
    List<Song> GetSongs(string keyword);
    List<Song> GetSongs();
}
