using UnityEditor;
using UnityEngine;

public class WrenchInteraction : MonoBehaviour
{
    public LayerMask WrenchMask;
    public DisableLeak disableLeak;
    public TimerQuest4 timer;
    bool HasWrench = false, FixedPipe = false, HasDoorOpener = false, OpenedDoors = false;
    public PlayerText playerText;
    public InGamePauseMenu pause;
    public DisableLeak dl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerText = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>(); // fails?
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Camera cam = Camera.main;
            Vector3 rayOrigin = cam.transform.position;
            Vector3 rayDirection = cam.transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * 10, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, WrenchMask))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag("Wrench") && !HasWrench) // even if we destroy it is failsafe too
                {
                    HasWrench = true;
                    Destroy(obj); // we got it
                    playerText.ShowText("Wrench acquired");
                } else if(obj.CompareTag("Pipe") && !FixedPipe)
                {
                    if(!HasWrench)
                    {
                        playerText.ShowText("You need wrench to fix the pipe!");
                        return;
                    }
                    if(!disableLeak.IsPumpOff)
                    {
                        pause.RestartGame();
                        return;
                    }
                    disableLeak.Disable(true);
                    timer.Stop(); // end timer countdown
                    FixedPipe = true;
                    playerText.ShowText("Pipe is fixed! No more gas.");
                } else if(obj.CompareTag("DoorOpener") && !HasDoorOpener)
                {
                    HasDoorOpener = true;
                    playerText.ShowText("Door opener acquired");
                    Destroy(obj);
                } else if(obj.CompareTag("Door") && HasDoorOpener && !OpenedDoors  && dl.IsFixed)
                {
                    obj.transform.parent.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
                    playerText.ShowText("Opened doors");
                    OpenedDoors = true;
                }
            }
        }
    }
}
