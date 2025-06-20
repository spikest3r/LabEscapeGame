using System;
using UnityEngine;

public class Quest2ColliderEvent : MonoBehaviour
{
    public MoveDetectionGame MDGObject;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Activated");
        MDGObject.SetState(true);
    }
}
