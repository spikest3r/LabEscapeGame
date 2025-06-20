using System;
using TMPro;
using UnityEngine;

public class CodeReveal : MonoBehaviour
{
    public TMP_Text text;
    public string code = "";
    char[] codeArray;
    bool[] revealed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        codeArray = code.ToCharArray();
        revealed = new bool[codeArray.Length];
        text.text = Format();
    }

    public void Reveal(int digit)
    {
        if (digit > codeArray.Length) return;
        revealed[digit] = true;
        text.text = Format();
    }

    string Format()
    {
        Func<int, char> getC = i => revealed[i] ? codeArray[i] : '_';
        return string.Format("Code: {0} {1} {2} {3}", getC(0),getC(1),getC(2),getC(3)); // complex
    }
}
