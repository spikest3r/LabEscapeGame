using UnityEngine;

public class TriggerHandlerMG3 : MonoBehaviour
{
    public bool IsPlayer = false;
    public CatchAColorGame game;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer)
        {
            game.IsPlayerInRange = true;
        }
        else
        {
            game.RightTime = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer)
        {
            game.IsPlayerInRange = false;
        }
        else
        {
            game.RightTime = false;
        }
    }
}
