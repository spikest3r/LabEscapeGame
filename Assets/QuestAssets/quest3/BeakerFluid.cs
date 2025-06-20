using System.Collections.Generic;
using UnityEngine;

public class BeakerFluid : MonoBehaviour
{
    [SerializeField] MeshRenderer liquid = null, self = null;
    public List<string> potions { get; private set; }

    void Awake()
    {
        if (liquid == null) liquid = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        Debug.Log(liquid);
        self = GetComponent<MeshRenderer>();
        liquid.enabled = false;
        potions = new();
    }

    public void Fill(string potion)
    {
        liquid.enabled = true;
        potions.Add(potion);
    }

    public void Clear()
    {
        liquid.enabled = false;
        potions.Clear();
    }

    public void Visible(bool state, bool forceLiquidState = false)
    {
        self.enabled = state;
        liquid.enabled = forceLiquidState || state && potions.Count > 0;
    }
}
