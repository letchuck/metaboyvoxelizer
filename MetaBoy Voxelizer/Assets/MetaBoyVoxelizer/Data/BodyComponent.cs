using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BodyComponent : MonoBehaviour
{
    public event EventHandler AttackFinished;
    public event EventHandler UnitDied;

    public string BodyName = "";
    public SkinnedMeshRenderer Face;
    public bool bIsWhiteFace = false;
    public Transform HatPoint;
    public Transform LeftHandPoint;
    public Transform RightHandPoint;
    public Transform WaistPoint;
    public Transform BackPoint;

    public Skeleton SkeletonPoint;

    


    public string WeaponAnimTrigger = "";

    public void Death()
    {
        if(UnitDied != null)
        {
            UnitDied.Invoke(this, EventArgs.Empty);
        }
    }

    public void AttackOver()
    {
        if(AttackFinished != null)
        {
            AttackFinished.Invoke(this, EventArgs.Empty);
        }
    }

    public void LoadMetaBoy(MetaBoyData InData)
    {
        BindTransforms();
        LoadFace(InData.Face);
        if (InData.Hat != "")
        {
            LoadHat(InData.Hat);
        }
        if (InData.Weapon != "")
        {
            LoadWeapon(InData.Weapon);
        }
        if(InData.Back != "")
        {
            LoadBack(InData.Back);
        }
        if(InData.Waist != "")
        {
            LoadWaist(InData.Waist);
        }
        if (InData.Neck != "")
        {
            LoadNeck(InData.Neck);
        }
    }

    public bool LoadWaist(string InWaist)
    {
        WaistDefinition Waist = WaistDatabase.Instance.GetWaistDefinition(InWaist);
        if(Waist == null)
        {
            return false;
        }
        GameObject WaistObject = GameObject.Instantiate(Waist.WaistPrefab, this.transform);
        WearableComponent WaistWearable = WaistObject.GetComponent<WearableComponent>();
        if(WaistWearable.SkelSkin != null)
        {
            SkeletonPoint.EquipSkin(WaistWearable.SkelSkin);
        }
        Destroy(WaistObject, 0.1f);
        return true;
    }

    public bool LoadNeck(string InNeck)
    {
        NeckDefinition Neck = NeckDatabase.Instance.GetNeckDefinition(InNeck);
        if (Neck == null)
        {
            return false;
        }
        GameObject NeckObject = GameObject.Instantiate(Neck.NeckPrefab, this.transform);
        WearableComponent NeckWearable = NeckObject.GetComponent<WearableComponent>();
        if (NeckWearable.SkelSkin != null)
        {
            SkeletonPoint.EquipSkin(NeckWearable.SkelSkin);
        }
        Destroy(NeckObject, 0.1f);
        return true;
    }



    public void LoadMetaBoy(MetaBoyConfig InConfig)
    {
        BindTransforms();
        LoadFace(InConfig.Face);
        if(InConfig.Hat != "")
        {
            LoadHat(InConfig.Hat);
        }
        if(InConfig.Weapon != "")
        {
            LoadWeapon(InConfig.Weapon);
        }
        if (InConfig.Back != "")
        {
            LoadBack(InConfig.Back);
        }
        if (InConfig.Waist != "")
        {
            LoadWaist(InConfig.Waist);
        }
        if(InConfig.Neck != "")
        {
            LoadNeck(InConfig.Neck);
        }
    }

    public bool LoadWeapon(string InWeaponName)
    {
        WeaponDefinition Weapon = WeaponDatabase.Instance.GetWeaponDefinition(InWeaponName);
        if(Weapon == null)
        {
            return false;
        }
        GameObject WeaponObject = GameObject.Instantiate(Weapon.WeaponPrefab, this.transform.GetChild(0));
        WeaponComponent WeaponComp = WeaponObject.GetComponent<WeaponComponent>();
        WeaponComp.SnapWeapon(this);
        if(WeaponComp.SkelSkin != null)
        {
            SkeletonPoint.EquipSkin(WeaponComp.SkelSkin);
            Transform Arms = transform.Find("Model/Body.Arms");
            if(Arms)
            {
                Arms.gameObject.SetActive(false);
            }
        }
        return true;
        
    }

    public bool LoadHat(string InHatName)
    {
        HatDefinition Hat = HatDatabase.Instance.GetHatDefinition(InHatName);
        if(Hat == null)
        {
            return false;
        }
        GameObject HatObject = GameObject.Instantiate(Hat.HatPrefab, HatPoint);
        return true;
    }

    public bool LoadBack(string InBackName)
    {
        BackDefinition Back = BackDatabase.Instance.GetBackDefinition(InBackName);
        if(Back == null)
        {
            return false;
        }
        GameObject BackObject = GameObject.Instantiate(Back.BackPrefab, BackPoint);
        return true;
    }

    public bool LoadFace(string InFaceName)
    {
        Material BaseFace = new Material(Face.sharedMaterial);
        if (!FaceManager.Instance.Faces.GetFaceMaterial(InFaceName, ref BaseFace, bIsWhiteFace))
        {
            Debug.LogError("OH NO!");
            return false;
        }
        else
        {
            Face.sharedMaterial = BaseFace;
            return true;
        }
    }

    public void BindTransforms()
    {
        Face = transform.Find("Model/Body.Face").GetComponent<SkinnedMeshRenderer>();
        HatPoint = transform.Find("Model/Armature/hips/spine/chest/head/hat");
        LeftHandPoint = transform.Find("Model/Armature/hips/spine/chest/shoulder.L/arm.L/forearm.L/hand.L");
        RightHandPoint = transform.Find("Model/Armature/hips/spine/chest/shoulder.R/arm.R/forearm.R/hand.R");
        WaistPoint = transform.Find("Model/Armature/hips");

    }

    private void OnValidate()
    {
        BindTransforms();
        if(BodyName == "")
        {
            BodyName = this.name.Split('_')[1];
        }
        if(SkeletonPoint == null)
        {
            SkeletonPoint = this.gameObject.AddComponent<Skeleton>();
            SkeletonPoint.rootBone = WaistPoint;
            SkeletonPoint.PrecacheRig();            
        }
    }

    public Transform GetBone(string InBoneName)
    {
        Transform Bone = this.transform;
        if (InBoneName == "spine")
        {
            
            Bone = transform.Find("Model/Armature/hips/spine");
            return Bone;
        }
        Bone = transform.Find("Model/Armature/hips/spine/chest/" + InBoneName);
        return Bone;
    }
}
