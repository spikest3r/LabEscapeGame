using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotesGUI : MonoBehaviour
{
    public TMP_Text text;
    public Button closeButton;
    PlayerText playerText = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            playerText = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
        } catch(System.Exception)
        {
            Debug.LogWarning("No playerText");
            playerText = null;
        }
        Close();
        closeButton.onClick.AddListener(Close);
    }

    public void Open(string NoteText)
    {
        if (!GameController.AlreadyOpen)
        {
            if(playerText != null) playerText.ClearText();
            transform.localScale = Vector3.one;
            text.text = NoteText;
            GameController.SetGameState(true); // pause game
        }
    }

    public void Close()
    {
        transform.localScale = Vector3.zero;
        text.text = "";
        GameController.SetGameState(false, false); // unpause game
    }
}
