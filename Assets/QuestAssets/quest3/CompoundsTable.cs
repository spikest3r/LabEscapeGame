using Unity.VisualScripting;
using UnityEngine;

public class CompoundsTable : MonoBehaviour
{
    public GameObject[] Prefabs;
    public GameObject[] TheseObjects;
    int holdingIndex = -1; // -1 means no stuff holding

    public void PickUpPotion(string name)
    {
        for(int i = 0; i < TheseObjects.Length; i++)
        {
            PotionObject obj = TheseObjects[i].GetComponent<PotionObject>();
            if (obj.Name == name)
            {
                Debug.Log(obj.Name);
                obj.DestroySelf();
                holdingIndex = i;
                break;
            }
        }
    }

    public void PutDownPotion()
    {
        if(holdingIndex != -1)
        {
            TheseObjects[holdingIndex] = Instantiate(Prefabs[holdingIndex],transform);
            holdingIndex = -1;
        }
    }
}
