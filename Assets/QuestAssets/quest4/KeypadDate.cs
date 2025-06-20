using NavKeypad;
using System;
using UnityEngine;

public class KeypadDate : MonoBehaviour
{
    DateTime dt;
    public Keypad kp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dt = DateTime.Now;
        kp.keypadCombo = Convert.ToInt32(string.Format("{0}{1:00}{2}", dt.Date.Day, dt.Date.Month, dt.Date.Year));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
