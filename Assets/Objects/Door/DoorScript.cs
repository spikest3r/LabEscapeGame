using UnityEngine;

// helper class to use with keypad
public class DoorScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor() // set trigger
    {
        animator.SetTrigger("OpenDoor");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("CloseDoor");
    }
}
