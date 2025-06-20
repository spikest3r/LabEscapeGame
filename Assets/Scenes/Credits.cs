using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Button btn;
    public DarkenScript drk;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true;
        btn.onClick.AddListener(click);
    }

    void click()
    {
        drk.Begin();
        drk.OnExecuted += trans;
    }

    void trans()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
