using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
    public DoorScript door;
    public DarkenScript darken;
    bool CoroutineRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !CoroutineRunning)
        {
            StartCoroutine(NextLevel());
            CoroutineRunning = true;
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        door.CloseDoor();
        yield return new WaitForSeconds(2f);
        Debug.Log("Next level");
        darken.Begin();
        darken.OnExecuted += Executed;
    }

    void Executed()
    {
        GameController.PlayerData.Level++;
        SceneManager.LoadScene(GameController.PlayerData.LevelScene);
    }
}
