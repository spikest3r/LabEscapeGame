using UnityEngine;

public class EndMessage : MonoBehaviour
{
    public PlayerText text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.SetText("Oh my God! You managed to escape the lab! You're out now. And that's the end here, unfortunately... Step on the white platform to finish. Or roam around if you want, but you won't find anything interesting here!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
