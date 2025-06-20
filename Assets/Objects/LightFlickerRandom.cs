using System.Collections;
using UnityEngine;

public class LightFlickerRandom : MonoBehaviour
{
    public bool IsEnabled = true;
    public float Minimal = .2f, Maximal = 1f; // default is best

    Light lighting;
    float ogIntensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lighting = GetComponent<Light>();
        ogIntensity = lighting.intensity;
    }

    IEnumerator FlickLight()
    {
        while(IsEnabled)
        {
            yield return new WaitForSeconds(Random.Range(Minimal, Maximal));
            lighting.intensity = 0;
            yield return new WaitForSeconds(.1f);
            lighting.intensity = ogIntensity; // return back to original intensity
        }
        lighting.intensity = ogIntensity;
    }

    private void OnEnable()
    {
        StartCoroutine(FlickLight());
    }

    private void OnDisable()
    {
        StopCoroutine(FlickLight());
        lighting.intensity = ogIntensity;
    }
}
