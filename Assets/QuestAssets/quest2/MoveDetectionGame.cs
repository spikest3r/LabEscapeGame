using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelRestartOnFail))]
public class MoveDetectionGame : MonoBehaviour
{
    public bool Active {
        get;
        private set;
    }
    bool Deactivated;
    public Rigidbody player;
    public float Min = 2f, Max = 4f;
    public float MagnitudeThreshold;
    bool AllowedToMove = true;
    bool ActivatedBefore = false;
    public Animator RedOutline;
    public RawImage Crosshair;

    public void SetState(bool state)
    {
        if (!Deactivated)
        {
            Debug.Log(state ? "Enabled MDG" : "Disabled MDG");
            Active = state;
            Deactivated = !state;
            if (state) StartCoroutine(Wait());
            ActivatedBefore = true;
        }
    }
    private void Update()
    {
        if(Active)
        {
            if (!Input.GetKey(KeyCode.LeftControl) || (player.linearVelocity.magnitude >= MagnitudeThreshold && !AllowedToMove))
            {
                Debug.Log("Failed");
                SetState(false);
                GetComponent<LevelRestartOnFail>().Restart();
            }
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("Waiting for action");
        yield return new WaitForSeconds(Random.Range(Min, Max));
        if(Active) StartCoroutine(ActionStop());
    }

    IEnumerator ActionStop()
    {
        Debug.Log("Stop");
        StartCoroutine(BlinkCrosshair());
        RedOutline.SetBool("Visible", true);
        yield return new WaitForSeconds(.8f+.4f); // delay so player has time to stop
        Debug.Log("Stop .7f");
        AllowedToMove = false;
        yield return new WaitForSeconds(Random.Range(Min, Max));
        AllowedToMove = true;
        RedOutline.SetBool("Visible", false);
        if(Active) StartCoroutine(Wait());
    }

    IEnumerator BlinkCrosshair()
    {
        yield return new WaitForSeconds(0.2f);
        bool toggle = false;
        for(int i = 0; i < 4; i++)
        {
            toggle = !toggle;
            Crosshair.color = toggle ? Color.clear : Color.white;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
