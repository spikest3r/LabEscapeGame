using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public string Name;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
