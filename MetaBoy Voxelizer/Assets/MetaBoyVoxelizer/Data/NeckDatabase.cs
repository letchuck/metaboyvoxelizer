using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NeckDefinition
{
    [SerializeField] public string NeckName;
    [SerializeField] public GameObject NeckPrefab;
}

public class NeckDatabase
{
    private List<NeckDefinition> Necks = new List<NeckDefinition>();
    public static NeckDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NeckDatabase();
            }
            return instance;
        }
    }
    private static NeckDatabase instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new NeckDatabase();
        }
    }

    public NeckDatabase()
    {
        Necks = new List<NeckDefinition>();
        var ExistingNecks = Resources.LoadAll<GameObject>("Necks");
        if (ExistingNecks != null && ExistingNecks.Length > 0)
        {
            foreach (var Waist in ExistingNecks)
            {
                WearableComponent Comp = Waist.GetComponent<WearableComponent>();
                NeckDefinition NewNeck = new NeckDefinition();
                NewNeck.NeckName = Comp.WearableName.ToUpper();
                NewNeck.NeckName = NewNeck.NeckName.Replace(" ", "");
                NewNeck.NeckPrefab = Waist;
                Necks.Add(NewNeck);
            }
        }
    }

    public NeckDefinition GetNeckDefinition(string InNeckName)
    {
        if(InNeckName == null)
        {
            return null;
        }
        InNeckName = InNeckName.Replace(" ", "");
        InNeckName = InNeckName.ToUpper();
        foreach (var Waist in Necks)
        {
            if (Waist.NeckName == InNeckName)
            {
                return Waist;
            }
        }
        return null;
    }
}