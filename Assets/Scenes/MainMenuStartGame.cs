using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStartGame : MonoBehaviour
{
    public Button ButtonStart, ButtonCredits;
    public DarkenScript darkenObject;
    bool Erased = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonStart.transform.GetChild(0).GetComponent<TMP_Text>().text = string.Format("Level {0}", GameController.PlayerData.Level);
        ButtonStart.onClick.AddListener(BeginDarken);
        ButtonCredits.onClick.AddListener(CreditsClick);
    }

    void CreditsClick()
    {
        Debug.Log("Credits()");
        Destroy(ButtonStart.gameObject);
        Destroy(ButtonCredits.gameObject);
        darkenObject.Begin();
        darkenObject.OnExecuted += CreditsTransition;
    }

    void CreditsTransition()
    {
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        if(Keyboard.current.qKey.isPressed && Keyboard.current.periodKey.isPressed && Keyboard.current.spaceKey.isPressed)
        {
            if(!Erased)
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Debug.LogWarning("Erased PlayerPrefs");
                ButtonStart.transform.GetChild(0).GetComponent<TMP_Text>().text = string.Format("Level {0}", GameController.PlayerData.Level);
                Erased = true;
            }
        } else
        {
            if (Erased) Erased = false;
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(GameController.PlayerData.LevelScene);
    }

    void BeginDarken()
    {
        Debug.Log("BeginDarken()");
        Destroy(ButtonStart.gameObject);
        Destroy(ButtonCredits.gameObject);
        darkenObject.Begin();
        darkenObject.OnExecuted += ChangeScene;
    }
}
