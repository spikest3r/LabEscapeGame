using TMPro;
using UnityEngine;

public class NoteReaderController : MonoBehaviour
{
    public LayerMask NotesLayerMask;
    public NotesGUI notesGUI;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Camera cam = Camera.main;
            Vector3 rayOrigin = cam.transform.position;
            Vector3 rayDirection = cam.transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * 10, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, NotesLayerMask))
            {
                GameObject obj = hit.collider.gameObject;
                Debug.Log(obj.tag);
                if (obj.CompareTag("Note"))
                {
                    Note note = obj.GetComponent<Note>();
                    notesGUI.Open(note.Text);
                }
            }
        }

    }
}
