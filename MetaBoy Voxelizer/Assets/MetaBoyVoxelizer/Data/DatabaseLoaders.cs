using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseLoaders : MonoBehaviour
{
    public bool LoadOnAwake = false;

    private void Awake()
    {
        if(LoadOnAwake)
        {
            LoadDatabases();
        }
    }

    public void LoadDatabases()
    {
        BackDatabase.Cache();
        BodyDatabase.Cache();
        FaceManager.Cache();
        HatDatabase.Cache();
        NeckDatabase.Cache();
        WaistDatabase.Cache();
        WeaponDatabase.Cache();
    }
}
