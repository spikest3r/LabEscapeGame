using UnityEngine;

public class LevelRestartOnFail : MonoBehaviour
{
    public InGamePauseMenu pauseMenu; 
    public void Restart()
    {
        pauseMenu.Darken.Speed = 0.02f; // lower slightly
        pauseMenu.RestartGame();
    }
}
