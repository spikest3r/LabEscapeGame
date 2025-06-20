using UnityEngine;

public class FlashlightInteraction : MonoBehaviour
{
    public LayerMask FlashlightMask;
    public Light Flashlight;
    bool HasFlashlight = false;
    public PlayerText text;

    void Start()
    {
        Flashlight.enabled = false;
        text = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HasFlashlight && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Flashlight.enabled = !Flashlight.enabled;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Camera cam = Camera.main;
            Vector3 rayOrigin = cam.transform.position;
            Vector3 rayDirection = cam.transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * 10, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, FlashlightMask))
            {
                GameObject obj = hit.collider.gameObject;
                if(!HasFlashlight)
                {
                    HasFlashlight = true;
                    Destroy(obj);
                    text.ShowText("Acquired flashlight. Press '1' to toggle it.");
                }
            }
        }
    }
}
