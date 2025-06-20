using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePauseMenu : MonoBehaviour
{
    public Button Continue, Restart, Exit;
    public DarkenScript Darken;
    bool ExecuteAction = false;
    string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.zero;
        Continue.onClick.AddListener(SwitchPause);
        Restart.onClick.AddListener(RestartGame);
        Exit.onClick.AddListener(ExitGame);
        Darken.OnExecuted += DarkenAction;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    void SwitchPause()
    {
        bool IsHidden = transform.localScale == Vector3.zero;
        if (IsHidden && GameController.AlreadyOpen) return;
        transform.localScale = IsHidden ? Vector3.one : Vector3.zero;
        GameController.SetGameState(IsHidden); // pause if hidden
    }

    void ExitGame()
    {
        SceneName = "MainMenu";
        ExecuteAction = true;
        Darken.Begin(false);
    }

    // used for other things
    public void RestartGame()
    {
        SceneName = SceneManager.GetActiveScene().name;
        ExecuteAction = true;
        Darken.Begin(false);
    }

    void DarkenAction()
    {
        if (ExecuteAction)
        {
            Time.timeScale = 1; // bug was here
            SceneManager.LoadScene(SceneName);
        }
    }
}
