using System.Collections;
using UnityEngine;

public class LevelEntranceHandler : MonoBehaviour
{
    public DoorScript door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(2f);
        door.OpenDoor();
    }
}
