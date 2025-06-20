using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    [SerializeField] DarkenScript darken;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("Player"))
        {
            darken.Begin();
            darken.OnExecuted += Transition;
        }
    }

    void Transition()
    {
        SceneManager.LoadScene("Credits");
    }
}
