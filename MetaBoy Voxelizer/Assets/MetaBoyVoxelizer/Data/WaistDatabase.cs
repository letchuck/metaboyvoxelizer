using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WaistDefinition
{
    [SerializeField] public string WaistName;
    [SerializeField] public GameObject WaistPrefab;
}

public class WaistDatabase
{
    private List<WaistDefinition> Waists = new List<WaistDefinition>();
    public static WaistDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WaistDatabase();
            }
            return instance;
        }
    }
    private static WaistDatabase instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new WaistDatabase();
        }
    }

    public WaistDatabase()
    {
        Waists = new List<WaistDefinition>();
        var ExistingWaists = Resources.LoadAll<GameObject>("Waists");
        if (ExistingWaists != null && ExistingWaists.Length > 0)
        {
            foreach (var Waist in ExistingWaists)
            {
                WearableComponent Comp = Waist.GetComponent<WearableComponent>();
                WaistDefinition NewWaist = new WaistDefinition();
                NewWaist.WaistName = Comp.WearableName.ToUpper();
                NewWaist.WaistName = NewWaist.WaistName.Replace(" ", "");
                NewWaist.WaistPrefab = Waist;
                Waists.Add(NewWaist);
            }
        }
    }

    public WaistDefinition GetWaistDefinition(string InWaistName)
    {
        if(InWaistName == null)
        {
            return null;
        }
        InWaistName = InWaistName.Replace(" ", "");
        InWaistName = InWaistName.ToUpper();
        foreach (var Waist in Waists)
        {
            if (Waist.WaistName == InWaistName)
            {
                return Waist;
            }
        }
        return null;
    }
}