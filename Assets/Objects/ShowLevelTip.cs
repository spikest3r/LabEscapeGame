using UnityEngine;

public class ShowLevelTip : MonoBehaviour
{
    [Multiline] public string Tip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("PlayerTextTip").GetComponent<PlayerText>().ShowText(Tip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
