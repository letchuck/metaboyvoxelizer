using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraitLister : MonoBehaviour
{
    public GameObject TraitPrefab;
    public List<GameObject> Traits = new List<GameObject>();
    public Color TraitLabel;
    public Color TraitType;
    bool TraitDisplay = false;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public static TraitLister Instance
    {
        get
        {
            return instance;
        }

    }
    private static TraitLister instance;

    public static void AddTrait(string InTraitName, string InTraitValue)
    {
        if(InTraitValue == "")
        {
            return;
        }
        if(!Instance)
        {
            return;
        }
        GameObject Trait = Instantiate(Instance.TraitPrefab, Instance.transform.parent);
        if(Instance.Traits.Count == 0)
        {
            Trait.transform.SetSiblingIndex(Instance.transform.GetSiblingIndex() + 1);
        }
        else
        {
            Trait.transform.SetSiblingIndex(Instance.Traits[Instance.Traits.Count - 1].transform.GetSiblingIndex() + 1);
        }
        Trait.GetComponent<TMP_Text>().text = InTraitName.Colorize(Instance.TraitLabel) + " " + InTraitValue.Colorize(Instance.TraitType);
        Trait.SetActive(Instance.TraitDisplay);
        Instance.Traits.Add(Trait);
    }

    public static void ToggleTraits(bool inState)
    {
        Instance.TraitDisplay = inState;
        foreach (GameObject Trait in Instance.Traits)
        {
            Trait.SetActive(inState);
        }
    }

    public static void ClearTraits()
    {
        if (!Instance)
        {
            return;
        }
        if (Instance.Traits.Count == 0)
        {
            return;
        }
        for(int TraitIndex = Instance.Traits.Count - 1; TraitIndex >= 0; TraitIndex--)
        {
            Destroy(Instance.Traits[TraitIndex]);
        }
        Instance.Traits = new List<GameObject>();
    }
}
