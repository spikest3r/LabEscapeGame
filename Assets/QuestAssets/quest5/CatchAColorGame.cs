using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CatchAColorGame : MonoBehaviour
{
    public GameObject CubePrefab;
    public int Tries = 6;
    int CurrentTries = 10;
    KeyCode ExpectedKey;
    public bool IsPlayerInRange = false;
    bool IsGameRunning = false;
    Color[] colors;
    Func<int, KeyCode> GetKey;
    public bool RightTime = false;
    public bool Fallen = false;
    PlayerText text;
    bool Won = false;
    public CodeReveal codeReveal;

    private void Awake()
    {
        colors = new Color[] { Color.red, Color.green, Color.blue };
        GetKey = i => i == 0 ? KeyCode.R : i == 1 ? KeyCode.G : i == 2 ? KeyCode.B : KeyCode.Asterisk;
    }

    private void Start()
    {
        text = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && IsPlayerInRange && !IsGameRunning && !Won)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        Tries = CurrentTries;
        IsGameRunning = true;
        Fallen = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Game());
    }

    int PreviousValue = 0;
    int GenerateUniqueColor()
    {
        int value = 0;
        while(value == PreviousValue)
        {
            value = Random.Range(0, 3);
        }
        PreviousValue = value;
        return value;
    }

    IEnumerator Game()
    {
        while(IsGameRunning)
        {
            GameObject cube = Instantiate(CubePrefab, transform, false);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            MeshRenderer mr = cube.GetComponent<MeshRenderer>();
            cube.GetComponent<CubeFall>().game = this;
            int c = GenerateUniqueColor();
            mr.material.color = colors[c];
            ExpectedKey = GetKey(c);
            rb.isKinematic = false;
            CurrentTries--;
            yield return new WaitUntil(() => Input.GetKeyDown(ExpectedKey) || Fallen);
            Destroy(cube);
            if (Fallen)
            {
                IsGameRunning = false;
                text.ShowText("Failed! Restart minigame to try again.");
                break;
            }
            if (!RightTime)
            {
                IsGameRunning = false;
                text.ShowText("Too soon! Restart minigame to try again.");
                break;
            }
            if (CurrentTries <= 0)
            {
                Debug.Log("Success mg3");
                text.ShowText("Well done!");
                codeReveal.Reveal(2);
                Won = true;
                break;
            }
            yield return new WaitForSeconds(Random.Range(.2f,.5f));
        }  
    }
}
