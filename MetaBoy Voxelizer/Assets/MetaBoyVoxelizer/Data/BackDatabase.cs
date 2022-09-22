using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BackDefinition
{
    [SerializeField] public string BackName;
    [SerializeField] public GameObject BackPrefab;
}

public class BackDatabase
{
    private List<BackDefinition> Backs = new List<BackDefinition>();
    public static BackDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackDatabase();
            }
            return instance;
        }
    }
    private static BackDatabase instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new BackDatabase();
        }
    }
    public BackDatabase()
    {
        Backs = new List<BackDefinition>();
        var ExistingBacks = Resources.LoadAll<GameObject>("Backs");
        if (ExistingBacks != null && ExistingBacks.Length > 0)
        {
            foreach (var Back in ExistingBacks)
            {
                WearableComponent Comp = Back.GetComponent<WearableComponent>();
                BackDefinition NewBack = new BackDefinition();
                NewBack.BackName = Comp.WearableName.ToUpper();
                NewBack.BackName = NewBack.BackName.Replace(" ", "");
                NewBack.BackPrefab = Back;
                Backs.Add(NewBack);
            }
        }
    }

    public BackDefinition GetBackDefinition(string InBackName)
    {
        if (InBackName == null)
        {
            return null;
        }
        InBackName = InBackName.Replace(" ", "");
        InBackName = InBackName.ToUpper();
        foreach (var Back in Backs)
        {
            if (Back.BackName == InBackName)
            {
                return Back;
            }
        }
        return null;
    }
}