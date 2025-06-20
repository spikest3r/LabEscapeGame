using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// sorry
public class DarkenScript : MonoBehaviour
{
    public float Speed = 0.05f;
    public bool StartOnAwake = false;
    public Action OnExecuted;

    public bool IsRunning { get; private set; } = false;
    public bool HasExecuted { get; private set; } = false;

    RawImage Object;
    bool IsStartingStateDark = false;
    float Multiplier = 1f;
    float StartDelay;

    bool IsConditionNotYetMet(float value) // neat
    {
        return IsStartingStateDark ? value > 0f : value < 1f;
    }

    public void Begin(bool delay = true)
    {
        if(HasExecuted)
        {
            // recalculate
            IsStartingStateDark = Object.color.a > 0.5; // if its more than half, then its opaque
            Multiplier = IsStartingStateDark ? 1 : -1;
            Debug.Log(string.Format("{0}, {1}", IsStartingStateDark, Multiplier));
        }
        IsRunning = true;
        HasExecuted = false;
        transform.localScale = Vector3.one;
        StartDelay = delay ? 2f : 0f;
        StartCoroutine(DarkenRoutine());
    }

    void Awake()
    {
        if(StartOnAwake)
        {
            Begin(false); // skip delay
        }
    }

    void Start()
    {
        // easiest way to find out all stuff instead linking in inspector
        Object = GetComponent<RawImage>();
        IsStartingStateDark = Object.color.a > 0.5; // if its more than half, then its opaque
        Multiplier = IsStartingStateDark ? 1 : -1;
    }

    IEnumerator DarkenRoutine()
    {
        Debug.Log("Coroutine Started");
        yield return new WaitForSeconds(StartDelay);
        Debug.Log("StartDelay passed");
        while(IsRunning)
        {
            if (IsConditionNotYetMet(Object.color.a))
            {
                Color c = Object.color;
                c.a -= Speed * Multiplier;
                Object.color = c;
            }
            else
            {
                IsRunning = false;
                HasExecuted = true;
                OnExecuted?.Invoke();
            }
            yield return null;
        }
        Debug.Log("IsRunning == false");
        if(IsStartingStateDark)
        {
            transform.localScale = Vector3.zero;
        }
    }
}
