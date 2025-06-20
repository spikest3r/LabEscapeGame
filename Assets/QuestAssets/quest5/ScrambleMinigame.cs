using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleMinigame : MonoBehaviour
{
    public LayerMask NotesLayerMask;
    public GameObject GUI;
    public Button Try, Close;
    public TMP_InputField word;
    public TMP_Text text;
    public PlayerText pText;
    public CodeReveal cr;
    bool solved = false;
    bool opened = false;

    string[] scrambles = { "atsfc", "napda", "lagratilo" };
    string[] words = { "facts", "panda", "aligator" };

    int index;

    void Awake()
    {
        GUI.transform.localScale = Vector3.zero;
        Close.onClick.AddListener(Toggle);
        Try.onClick.AddListener(TryVoid);
    }
    
    void TryVoid()
    {
        string guess = word.text;
        if(guess == words[index])
        {
            solved = true;
            Toggle();
            pText.ShowText("Well done!");
            cr.Reveal(3);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = Random.Range(0, 3);
        pText = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Camera cam = Camera.main;
            Vector3 rayOrigin = cam.transform.position;
            Vector3 rayDirection = cam.transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * 10, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, NotesLayerMask))
            {
                if(hit.collider.gameObject.CompareTag("Scramble") && !solved && !opened)
                {
                    Toggle();
                }
            }
        }
    }

    void Toggle()
    {
        opened = !opened;
        text.text = string.Format("Unscramble the word:\n{0}", scrambles[index]);
        GUI.transform.localScale = opened ? Vector3.one : Vector3.zero;
        GameController.SetGameState(opened);
    }
}
