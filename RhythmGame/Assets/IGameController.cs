using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void StartSong(Song song);
    void PauseGame();
    void ResumeGame();
    bool IsRunning();

}
