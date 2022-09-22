using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public class MetaBoyData
{
    [Header("Attributes")]
    [SerializeField] public string Back = "";
    [SerializeField] public string Body = "";
    [SerializeField] public string Face = "";
    [SerializeField] public string Hat = "";
    [SerializeField] public string Neck = "";
    [SerializeField] public string Weapon = "";
    [SerializeField] public string Waist = "";
    [SerializeField] public string Background = "";
    [SerializeField] public string ID = "UNKNOWN";

    public MetaBoyData()
    {
        Back = "";
        Body = "";
        Face = "";
        Hat = "";
        Neck = "";
        Weapon = "";
        Waist = "";
        Background = "";
        ID = "UNSET";
    }

    public MetaBoyData(metadataJson InMetaData) : this()
    {
        foreach (var MetaAttribute in InMetaData.attributes)
        {
            // We skip null things.
            if(MetaAttribute.value == null)
            {
                Debug.Log("Skipped: " + MetaAttribute.trait_type);
                continue;
            }

            if (MetaAttribute.trait_type == "Back")
            {
                this.Back = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Body")
            {
                this.Body = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Face")
            {
                this.Face = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Hat")
            {
                this.Hat = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Neck")
            {
                this.Neck = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Weapon")
            {
                this.Weapon = MetaAttribute.value.ToUpper();
            }
            else if (MetaAttribute.trait_type == "Waist")
            {
                this.Waist = MetaAttribute.value.ToUpper();
            }
            else if(MetaAttribute.trait_type == "Background")
            {
                this.Background = MetaAttribute.value.ToUpper();
            }
        }
        ID = InMetaData.name;
    }
}

[Serializable]
[CreateAssetMenu(fileName = "MetaBoy", menuName = "Thadeus' Last Stand/MetaBoy", order = 0)]
public class MetaBoyConfig : ScriptableObject
{
    [Header("Selection")]
    [ListToPopup(typeof(MetaStringData), "bodyList")]
    [SerializeField] public string BodyChoice;
    [ListToPopup(typeof(MetaStringData), "faceList")]
    [SerializeField] public string FaceChoice;
    [ListToPopup(typeof(MetaStringData), "hatList")]
    [SerializeField] public string HatChoice;
    [ListToPopup(typeof(MetaStringData), "weaponList")]
    [SerializeField] public string WeaponChoice;
    [ListToPopup(typeof(MetaStringData), "backList")]
    [SerializeField] public string BackChoice;
    [ListToPopup(typeof(MetaStringData), "waistList")]
    [SerializeField] public string WaistChoice;
    [ListToPopup(typeof(MetaStringData), "neckList")]
    [SerializeField] public string NeckChoice;

   

    [Header("Attributes")]
    [SerializeField] public string Back;
    [SerializeField] public string Body;
    [SerializeField] public string Face;
    [SerializeField] public string Hat;
    [SerializeField] public string Neck;
    [SerializeField] public string Weapon;
    [SerializeField] public string Waist;
    [SerializeField] public string ID;
    [SerializeField] public string Background;

    //[ContextMenu("Apply All")]
    [Button]
    public void ApplyChoices()
    {
        Body = BodyChoice;
        Face = FaceChoice;
        Hat = HatChoice;
        if(Hat == "NONE")
        {
            Hat = "";
        }
        Weapon = WeaponChoice;
        if (Weapon == "NONE")
        {
            Weapon = "";
        }
        Back = BackChoice;
        if(Back == "NONE")
        {
            Back = "";
        }
        Waist = WaistChoice;
        if (Waist == "NONE")
        {
            Waist = "";
        }
        Neck = NeckChoice;
        if (Neck == "NONE")
        {
            Neck = "";
        }
    }
    
    //[ContextMenu("Apply Body")]
    [Button]
    public void ApplyBody()
    {
        Body = BodyChoice;
    }

    //[ContextMenu("Apply Face")]
    [Button]
    public void ApplyFace()
    {
        Face = FaceChoice;
    }


    //[ContextMenu("Apply Hat")]
    [Button]
    public void ApplyHat()
    {
        Hat = HatChoice;
        if (Hat == "NONE")
        {
            Hat = "";
        }
    }

    //[ContextMenu("Apply Weapon")]
    [Button]
    public void ApplyWeapon()
    {
        Weapon = WeaponChoice;
        if (Weapon == "NONE")
        {
            Weapon = "";
        }
    }

    [Button]
    public void ApplyBack()
    {
        Back = BackChoice;
        if (Back == "NONE")
        {
            Back = "";
        }
    }

    [Button]
    public void ApplyWaist()
    {
        Waist = WaistChoice;
        if(Waist == "NONE")
        {
            Waist = "";
        }
    }

    [Button]
    public void ApplyNeck()
    {
        Neck = NeckChoice;
        if (Neck == "NONE")
        {
            Neck = "";
        }
    }

    public MetaBoyData CreateData()
    {
        MetaBoyData OutputData = new MetaBoyData();
        OutputData.Back = Back;
        OutputData.Body = Body;
        OutputData.Face = Face;
        OutputData.Hat = Hat;
        OutputData.Neck = Neck;
        OutputData.Weapon = Weapon;
        OutputData.Waist = Waist;
        OutputData.ID = ID;
        OutputData.Background = Background;
        return OutputData;
    }
}
