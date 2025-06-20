using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light[] lights;
    public Material Neon;
    public bool StartingState = true;

    void Start()
    {
        BulkState(StartingState);
    }

    public void BulkState(bool state)
    {
        foreach(Light light in lights)
        {
            light.gameObject.GetComponent<LightFlickerRandom>().IsEnabled = state;
            light.enabled = state;
        }
    }
}
