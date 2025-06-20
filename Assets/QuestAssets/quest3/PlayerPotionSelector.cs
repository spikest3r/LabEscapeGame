using UnityEngine;

public class PlayerPotionSelector : MonoBehaviour
{
    public LayerMask PotionLayerMask;
    public CompoundsTable table;

    PlayerText playerText;
    RaycastHit[] RayHits = new RaycastHit[10];
    Camera cam;
    bool IsForBroken = false;
    bool Holding = false;
    string PotionHeld = null;
    GameObject Beaker = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        playerText = GameObject.Find("PlayerTextTip").GetComponent<PlayerText>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = cam.transform.position;
        Vector3 rayDirection = cam.transform.forward;
        int HitsCount = Physics.RaycastNonAlloc(rayOrigin, rayDirection, RayHits, 3f, PotionLayerMask);
        IsForBroken = false;
        for(int i = 0; i < HitsCount; i++)
        {
            GameObject obj = RayHits[i].collider.gameObject;
            if (!Holding)
            {
                if (obj.CompareTag("Potion"))
                {
                    string name = obj.GetComponent<PotionObject>().Name;
                    playerText.SetText(name, true);
                    if (Input.GetMouseButtonDown(1))
                    {
                        table.PickUpPotion(name);
                        playerText.SetText(string.Format("Holding {0}", name), true);
                        Holding = true;
                        PotionHeld = name;
                        return;
                    }
                    IsForBroken = true;
                    break;
                } else if(obj.CompareTag("Beaker"))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        Beaker = obj;
                        obj.GetComponent<BeakerFluid>().Visible(false);
                        Holding = true;
                        playerText.SetText(string.Format("Holding {0}", "Beaker"), true);
                        return;
                    }
                }
            }
        }
        if(Holding)
        {
            if(Input.GetMouseButtonDown(1))
            {
                if (Beaker != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, rayDirection, out hit, 3f, PotionLayerMask))
                    {
                        GameObject obj = hit.collider.gameObject;
                        if(obj.CompareTag("Machine"))
                        {
                            MixingGameQuest3 machine = obj.GetComponent<MixingGameQuest3>();
                            machine.Load(Beaker);
                            playerText.ClearText();
                        } else
                        {
                            Beaker.GetComponent<BeakerFluid>().Visible(true);
                            Debug.Log("Return beaker");
                        }
                    } else
                    {
                        Beaker.GetComponent<BeakerFluid>().Visible(true);
                        Debug.Log("Return beaker");
                    }
                    Holding = false;
                    Beaker = null;
                    playerText.ClearText();
                }
                else
                {
                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, rayDirection, out hit, 3f, PotionLayerMask))
                    {
                        GameObject obj = hit.collider.gameObject;
                        if (obj.CompareTag("Beaker"))
                        {
                            obj.GetComponent<BeakerFluid>().Fill(PotionHeld);
                            playerText.ClearText();
                            playerText.ShowText(string.Format("Pouring {0} into beaker", PotionHeld));
                        }
                    }
                    table.PutDownPotion();
                    Holding = false;
                    PotionHeld = null;
                }
            }
        }
        if(!IsForBroken && !Holding && !playerText.TextClear && !playerText.ShowingText)
        {
            playerText.ClearText();
        }
    }
}
