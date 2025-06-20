using UnityEngine;

public class SceneHelper : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        GameController.Initialize(player);
        GameController.SetGameState(false); // just in case
        GameController.AlreadyOpen = false;
    }

    private void Update()
    {
        if(GameController.Paused && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } else if(!GameController.Paused && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
