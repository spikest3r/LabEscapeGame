using NavKeypad;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuessANumberGame : MonoBehaviour
{
    int code;
    public Keypad kp;
    public CodeReveal codeReveal;
    PlayerText text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
        code = Random.Range(1000, 2001);
        kp.keypadCombo = code;
    }

    public void ReportAccept()
    {
        codeReveal.Reveal(0); // reveal first digit
    }

    public void ReportDenied()
    {
        int guess = Convert.ToInt32(kp.currentInput);
        string txt = guess > code ? "Too big" : "Too small";
        text.ShowText(txt);
    }
}
