using IEnumerator = System.Collections.IEnumerator;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysGame : MonoBehaviour
{
    public int MaximumPatternCount = 5;
    Dictionary<string,GameObject> cubes;
    string[] keys;
    List<string> Combo;
    int CombosCount = 1;
    int ComboIndex = 0;
    public CodeReveal codeReveal;

    public bool IsGameRunning { private set; get; }
    public bool CanInput { private set; get; }
    bool activatedBefore = false;

    void Start()
    {
        cubes = new Dictionary<string,GameObject>();
        keys = new string[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            string name = obj.name;
            cubes.Add(name, obj);
            keys[i] = name;
        }
        Combo = new List<string>();
    }

    string IndexToKey(int i)
    {
        return keys[i];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !IsGameRunning && !activatedBefore)
        {
            Debug.Log("running");
            IsGameRunning = true;
            activatedBefore = true;
            GeneratePattern(); // 
            StartCoroutine(Show());
        }
    }

    public void HandleInput(GameObject obj)
    {
        if (!CanInput) return;
        string name = obj.name;
        Debug.Log("Handling Input");
        Debug.Log(Combo[ComboIndex]);
        if (Combo[ComboIndex] == name)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.red;
            ComboIndex++;
            if(ComboIndex >= Combo.Count)
            {
                StartCoroutine(Upd());
            }
        } else
        {
            ComboIndex = 0; // clear stack index
            ClearGrid();
            StartCoroutine(Show());
        }
    }

    void ClearGrid()
    {
        for(int i = 0; i < cubes.Count; i++)
        {
            cubes[IndexToKey(i)].GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    void GeneratePattern()
    {
        Combo.Clear();
        int i = 0;
        while(i < CombosCount)
        {
            int j = Random.Range(0, cubes.Count);
            GameObject cube = cubes[IndexToKey(j)];
            string next = cube.name;
            if (Combo.Contains(next)) continue;
            Debug.Log(next);
            Combo.Add(next);
            i++;
        } 
    }

    IEnumerator Show()
    {
        CanInput = false;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < Combo.Count; i++)
        {
            Debug.Log(string.Format("showing {0}", i));
            cubes[Combo[i]].GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(1f);
        ClearGrid();
        CanInput = true;
    }

    IEnumerator Upd()
    {
        ComboIndex = 0;
        CombosCount++;
        if (CombosCount > MaximumPatternCount)
        {
            CanInput = false;
            IsGameRunning = false;
            yield return new WaitForSeconds(1f);
            ClearGrid();
            codeReveal.Reveal(1); // reveal second digit
            yield break;
        }
        CanInput = false;
        yield return new WaitForSeconds(1f);
        ClearGrid();
        GeneratePattern();
        yield return StartCoroutine(Show());
    }
}
