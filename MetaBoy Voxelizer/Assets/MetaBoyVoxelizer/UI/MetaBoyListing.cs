using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class ColorListing
{
    [SerializeField] public string BackgroundName;
    [SerializeField] public Color BgColor;

    public ColorListing()
    {
        BackgroundName = "";
        BgColor = Color.black;
        BgColor.a = 1.0f;
    }
}

public class MetaBoyListing : MonoBehaviour
{
    private MetaBoyData Metaboy;
    public TMP_Text MetaID;
    public List<Image> Backgrounds = new List<Image>();
    public Color DefaultBG;
    public List<ColorListing> BgColors;

    public void SetMetaBoy(MetaBoyData InData)
    {
        Metaboy = InData;
        MetaID.text = InData.ID;
        if (InData.Background != "")
        {
            Color bgColor = GetColorFromName(InData.Background);
            foreach (Image Background in Backgrounds)
            {
                Background.color = bgColor;
            }
        }        
    }

    public Color GetColorFromName(string InName)
    {
        foreach (ColorListing Listing in BgColors)
        {
            if(Listing.BackgroundName == InName)
            {
                return Listing.BgColor;
            }
        }
        return DefaultBG;
    }
    
    public void HandleClick()
    {
        TraitLister.ClearTraits();
        VisualizerSpawner.Spawn(Metaboy);
        TraitLister.AddTrait("Body:", Metaboy.Body);
        TraitLister.AddTrait("Face:", Metaboy.Face);
        TraitLister.AddTrait("Background:", Metaboy.Background);
        TraitLister.AddTrait("Hat:", Metaboy.Hat);
        TraitLister.AddTrait("Weapon:", Metaboy.Weapon);
        TraitLister.AddTrait("Neck:", Metaboy.Neck);
        TraitLister.AddTrait("Back:", Metaboy.Back);
        TraitLister.AddTrait("Waist:", Metaboy.Waist);
        GameObject NameLabel = GameObject.Find("MetaboyChooser");
        if(NameLabel)
        {
            NameLabel.GetComponent<TMP_Text>().text = Metaboy.ID;
        }
    }
}
