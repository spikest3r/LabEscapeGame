using UnityEngine;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using NavKeypad;
using System;
using Random = UnityEngine.Random;

public class MixingGameQuest3 : MonoBehaviour
{
    public string[] TargetPotions;
    public Keypad kp;
    public BeakerFluid BuiltIn; // for looks only
    public BeakerFluid OGBeaker;
    List<string> potions;
    public PlayerText playerText;
    public Note HiddenNote;
    string NoteText;

    public void Load(GameObject obj)
    {
        BuiltIn.Visible(true, true);
        BeakerFluid beakerFluid = obj.GetComponent<BeakerFluid>();
        potions = beakerFluid.potions;
        OGBeaker = beakerFluid;
        StartCoroutine(Analize());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potions = new();
        BuiltIn.Visible(false);
        int code = Random.Range(100000, 999999);
        char[] charArray = string.Format("The PIN is {0}", code).ToCharArray();
        Array.Reverse(charArray);
        NoteText = new string(charArray);
        kp.keypadCombo = code;
        HiddenNote.Text = "*&^*(*(_@#$!";
    }

    void ReturnBack(string message = null, bool end = false)
    {
        if (message != null) playerText.ShowText(message);
        OGBeaker.Clear();
        OGBeaker.Visible(!end);
        BuiltIn.Visible(false);
    }

    IEnumerator Analize()
    {
        yield return new WaitForSeconds(2f);
        if(potions.Count < TargetPotions.Length)
        {
            ReturnBack("Not enough potions added");
            yield break;
        }
        if (potions.Count > TargetPotions.Length)
        {
            ReturnBack("Too much potions added");
            yield break;
        }
        for(int i = 0; i < TargetPotions.Length; i++)
        {
            if (potions[i] != TargetPotions[i])
            {
                ReturnBack("Wrong sequence of potions");
                yield break;
            }
        }
        HiddenNote.Text = NoteText;
        ReturnBack("Mixture is good. Note has revealed its secret", true);
    }
}
