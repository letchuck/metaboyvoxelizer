using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class BodyDefinition
{
    public string BodyName;
    public GameObject BodyPrefab;
}


public class BodyDatabase
{
    private List<BodyDefinition> Bodies = new List<BodyDefinition>();
    public static BodyDatabase Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BodyDatabase();
            }
            return instance;
        }
    }
    private static BodyDatabase instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new BodyDatabase();
        }
    }

    public BodyDatabase()
    {
        Bodies = new List<BodyDefinition>();
        var ExistingBodies = Resources.LoadAll<GameObject>("Bodies");
        if (ExistingBodies != null && ExistingBodies.Length > 0)
        {
            foreach(var Body in ExistingBodies)
            {
                BodyComponent Comp = Body.GetComponent<BodyComponent>();
                BodyDefinition NewBody = new BodyDefinition();
                NewBody.BodyName = Comp.BodyName.ToUpper();
                NewBody.BodyPrefab = Body;               
                Bodies.Add(NewBody);
            }
        }
    }

    public BodyDefinition GetBodyDefinition(string InBodyName)
    {
        if (InBodyName == null)
        {
            return null;
        }
        InBodyName = InBodyName.Replace(" ", "");
        BodyDefinition BackupDef = null;
        foreach (var Body in Bodies)
        {
            if(Body.BodyName == InBodyName)
            {
                return Body;
            }
            if(Body.BodyName == "UNKNOWN")
            {
                BackupDef = Body;
            }
        }
        return BackupDef;
    }
}
