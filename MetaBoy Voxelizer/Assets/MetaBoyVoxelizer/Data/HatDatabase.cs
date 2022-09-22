using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HatDefinition
{
    [SerializeField] public string HatName;
    [SerializeField] public GameObject HatPrefab;
}

public class HatDatabase
{
    private List<HatDefinition> Hats = new List<HatDefinition>();
    public static HatDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HatDatabase();
            }
            return instance;
        }
    }
    private static HatDatabase instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new HatDatabase();
        }
    }

    public HatDatabase()
    {
        Hats = new List<HatDefinition>();
        var ExistingHats = Resources.LoadAll<GameObject>("Hats");
        if (ExistingHats != null && ExistingHats.Length > 0)
        {
            foreach (var Hat in ExistingHats)
            {
                WearableComponent Comp = Hat.GetComponent<WearableComponent>();
                HatDefinition NewHat = new HatDefinition();
                NewHat.HatName = Comp.WearableName.ToUpper();
                NewHat.HatName = NewHat.HatName.Replace(" ", "");
                NewHat.HatPrefab = Hat;
                Hats.Add(NewHat);
            }
        }
    }

    public HatDefinition GetHatDefinition(string InHatName)
    {
        if (InHatName == null)
        {
            return null;
        }
        InHatName = InHatName.Replace(" ", "");
        InHatName = InHatName.ToUpper();
        foreach (var Hat in Hats)
        {
            if (Hat.HatName == InHatName)
            {
                return Hat;
            }
        }
        return null;
    }
}