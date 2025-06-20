using UnityEngine;

public class SimonSaysInteract : MonoBehaviour
{
    public LayerMask SimonMask;
    public SimonSaysGame game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Camera cam = Camera.main;
            Vector3 rayOrigin = cam.transform.position;
            Vector3 rayDirection = cam.transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * 10, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, SimonMask)) {
                Debug.Log(hit.collider.gameObject.name);
                game.HandleInput(hit.collider.gameObject);
            }
        }
    }
}
