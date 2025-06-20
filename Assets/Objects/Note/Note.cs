using UnityEngine;

// container class to store note text
public class Note : MonoBehaviour
{
    [TextArea] // because unity is dumb
    public string Text;
}
