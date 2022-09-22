using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponDefinition
{
    [SerializeField] public string WeaponName;
    [SerializeField] public GameObject WeaponPrefab;
}

public class WeaponDatabase  
{
    private List<WeaponDefinition> Weapons = new List<WeaponDefinition>();
    public static WeaponDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WeaponDatabase();
            }
            return instance;
        }
    }
    private static WeaponDatabase instance;

    public static void Cache()
    {
        if(instance == null)
        {
            instance = new WeaponDatabase();
        }
    }

    public WeaponDatabase()
    {
        Weapons = new List<WeaponDefinition>();
        var ExistingWeapons = Resources.LoadAll<GameObject>("Weapons");
        if (ExistingWeapons != null && ExistingWeapons.Length > 0)
        {
            foreach (var Hat in ExistingWeapons)
            {
                WeaponComponent Comp = Hat.GetComponent<WeaponComponent>();
                WeaponDefinition NewWeapon = new WeaponDefinition();
                NewWeapon.WeaponName = Comp.WeaponName.ToUpper();
                NewWeapon.WeaponName = NewWeapon.WeaponName.Replace(" ", "");
                NewWeapon.WeaponPrefab = Hat;
                Weapons.Add(NewWeapon);
            }
        }
    }

    public WeaponDefinition GetWeaponDefinition(string InWeaponName)
    {
        if (InWeaponName == null)
        {
            return null;
        }
        InWeaponName = InWeaponName.Replace(" ", "");
        InWeaponName = InWeaponName.ToUpper();
        foreach (var Weapon in Weapons)
        {
            if (Weapon.WeaponName == InWeaponName)
            {
                return Weapon;
            }
        }
        return null;
    }
}
