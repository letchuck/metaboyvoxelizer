using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class nftAttribute
{
    [SerializeField] public string trait_type;
    [SerializeField] public string value;
}

public class nftAttributes
{
    [SerializeField] public nftAttribute Background;
    [SerializeField] public nftAttribute Body;
    [SerializeField] public nftAttribute Face;
    [SerializeField] public nftAttribute Hat;
    [SerializeField] public nftAttribute Weapon;
}

[Serializable]
public class metadataJson
{
    // Standard Metadata
    [SerializeField]public string name;
    [SerializeField] public string description;
    [SerializeField] public string image;
    [SerializeField] public string animation_url;
    [SerializeField] public string royalty_percentage;

    //public nftAttrib attributes;
    [SerializeField] public nftAttribute[] attributes;
}



