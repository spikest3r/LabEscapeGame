using NavKeypad;
using UnityEngine;

public class Quest5RandomCodeGenerator : MonoBehaviour
{
    public Keypad kp;
    public CodeReveal cr;

    private void Awake()
    {
        int code = Random.Range(1000, 9999);
        kp.keypadCombo = code;
        cr.code = code.ToString();
    }
}
