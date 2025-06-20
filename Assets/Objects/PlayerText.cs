using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    TMP_Text t;
    public bool TextClear { get; private set; } // dont show inspector, field only for Set/Clear
    public bool ShowingText { get; private set; }

    void Awake()
    {
        t = GetComponent<TMP_Text>();
        transform.localScale = Vector3.zero;
        TextClear = true;
    }

    IEnumerator Countdown(float time)
    {
        yield return new WaitForSeconds(4f);
        if (TextClear)
        {
            transform.localScale = Vector3.zero;
            t.text = "";
            ShowingText = false;
        }
        currentCoroutine = null;
    }

    Coroutine currentCoroutine;

    public void ShowText(string text)
    {
        if (!TextClear) TextClear = true;
        transform.localScale = Vector3.one;
        t.text = text;
        ShowingText = true;
        // when sending new command to show text eliminate previous instance
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Countdown(4f));
    }

    public void SetText(string text, bool interrupt = false)
    {
        if (ShowingText && !interrupt) return;
        transform.localScale = Vector3.one;
        t.text = text;
        TextClear = false;
    }

    public void ClearText()
    {
        transform.localScale = Vector3.zero;
        t.text = "";
        TextClear = true;
    }
}
