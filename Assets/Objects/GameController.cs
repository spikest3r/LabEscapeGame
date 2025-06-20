using UnityEngine;

// static helper class
public class GameController
{
    public static bool AllowMenusToOpen = true; // notes are not affected
    public static bool Paused = false;
    public static bool AlreadyOpen = false;

    static FirstPersonMovement playerMovement;
    static FirstPersonLook playerCamera;
    static Crouch playerCrouch;
    static Jump playerJump;
    static FirstPersonAudio playerAudio;
    static bool IsInitialized = false;

    // save file stuff
    public static class PlayerData
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level", 1);
            set {
                PlayerPrefs.SetInt("Level",  value);
                PlayerPrefs.Save();
                Debug.Log("Saved PlayerPrefs");
            }
        }
        public static bool HasGloves
        {
            get => PlayerPrefs.GetInt("HasGloves", 0) == 1;
            set
            {
                PlayerPrefs.SetInt("HasGloves", value ? 1 : 0);
                PlayerPrefs.Save();
                Debug.Log("Saved PlayerPrefs");
            }
        }
        public static bool HasUranium
        {
            get => PlayerPrefs.GetInt("HasUranium", 0) == 1;
            set
            {
                PlayerPrefs.SetInt("HasUranium", value ? 1 : 0);
                PlayerPrefs.Save();
                Debug.Log("Saved PlayerPrefs");
            }
        }

        public static string LevelScene
        {
            get => string.Format("Level{0}", Level);
        }
    }

    public static void Initialize(GameObject player)
    {
        playerMovement = player.GetComponent<FirstPersonMovement>();
        playerCamera = player.GetComponentInChildren<FirstPersonLook>();
        playerCrouch = player.GetComponent<Crouch>();
        playerJump = player.GetComponent<Jump>();
        playerAudio = player.GetComponentInChildren<FirstPersonAudio>();
        IsInitialized = true;
    }

    public static void SetGameState(bool paused, bool pauseTime = true)
    {
        if (IsInitialized)
        {
            Paused = paused;
            AlreadyOpen = paused;
            if(pauseTime || !paused) Time.timeScale = paused ? 0 : 1; // if unpause it has to revive timeScale
            try
            {
                playerCamera.enabled = !paused;
                playerMovement.enabled = !paused;
                playerCrouch.enabled = !paused;
                playerJump.enabled = !paused;
                playerAudio.enabled = !paused;
                playerAudio.MutePlayerAudio();
            } catch(System.Exception ex)
            {
                Debug.LogWarning(ex);
            }
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused;
            return;
        }
        Debug.LogError("GameController is not initialized!");
    }
}
