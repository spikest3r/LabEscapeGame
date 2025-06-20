using UnityEngine;

public class VaultQuest3Uranium : MonoBehaviour
{
    public PlayerText playerText;

    public void Add()
    {
        if(!GameController.PlayerData.HasGloves)
        {
            playerText.ShowText("You can't handle Uranium without gloves.");
            return;
        }
        playerText.ShowText("Acquired Uranium");
        GameController.PlayerData.HasUranium = true;
    }
}
