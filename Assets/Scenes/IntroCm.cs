using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCm : MonoBehaviour
{
    public DarkenScript darken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        darken.Begin();
        yield return new WaitForSeconds(3f);
        darken.Begin();
        darken.OnExecuted += onExec;
    }

    void onExec()
    {
        SceneManager.LoadScene("CommercialScene");
    }
}
