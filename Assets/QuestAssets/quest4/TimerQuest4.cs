using System;
using TMPro;
using UnityEngine;

public class TimerQuest4 : MonoBehaviour
{
    public float TimeGiven = 60f; // 1:00 mm:ss too much lol?
    bool StopFlag = false;
    public TMP_Text t;
    bool exec = false;
    public InGamePauseMenu pause;

    string FormatText() // cool formatting
    {
        int m = (int)Math.Floor(TimeGiven / 60f);
        int s = (int)Math.Floor(TimeGiven % 60f);
        return string.Format("No Air In: {0}:{1:00}", m, s);
    }

    // TimeGiven timer
    void OnTimerEnd()
    {
        pause.RestartGame();
    }

    public void Stop()
    {
        StopFlag = true;
        t.text = "";
        t.gameObject.transform.localScale = Vector3.zero; // hide because it may interfere with ui
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t.text = FormatText();
    }

    // Update is called once per frame
    // we do time here so its real time lol
    void Update()
    {
        if (!exec && !StopFlag) // firstly check if no exec and no stop, then either upd timer or exec. more efficient and cheaper
        {
            if (TimeGiven > 0f)
            {
                TimeGiven -= Time.deltaTime;
                t.text = FormatText();
            }
            else
            {
                OnTimerEnd();
                exec = true;
            }
        }
    }
}
