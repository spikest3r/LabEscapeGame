using UnityEngine;

public class CubeFall : MonoBehaviour
{
    public CatchAColorGame game;

    private void OnCollisionEnter(Collision collision)
    {
        game.Fallen = true;
    }
}
