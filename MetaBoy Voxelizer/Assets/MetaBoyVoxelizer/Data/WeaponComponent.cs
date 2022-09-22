using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnapBone
{
    none,
    forearm,
    arm,
    hand,
    fingers,
    back,
    armspecial
}

public class WeaponComponent : MonoBehaviour
{
    public string WeaponName;

    public Transform LeftSnap;
    public SnapBone LeftBone = SnapBone.none;

    public Transform RightSnap;
    public SnapBone RightBone = SnapBone.none;

    public string AnimationTrigger = "";

    public SkeletonSkin SkelSkin;

    
    public void SnapWeapon(BodyComponent InBody)
    {
        if(LeftBone == SnapBone.back)
        {
            LeftSnap.transform.parent = InBody.GetBone("spine");
            LeftSnap.transform.localPosition = Vector3.zero;
        }
        else if(LeftBone != SnapBone.none)
        {
            string bonedName = "shoulder.L/arm.L";
            if(LeftBone != SnapBone.arm)
            {
                bonedName += "/forearm.L";
                if(LeftBone != SnapBone.forearm)
                {
                    bonedName += "/hand.L";
                    if(LeftBone != SnapBone.hand)
                    {
                        bonedName += "/fingers.L";
                    }
                }
            }
            LeftSnap.transform.parent = InBody.GetBone(bonedName);
            LeftSnap.transform.localPosition = Vector3.zero;

        }

        if (RightBone == SnapBone.back)
        {
            RightSnap.transform.parent = InBody.GetBone("spine");
            RightSnap.transform.localPosition = Vector3.zero;
        }
        else if (RightBone != SnapBone.none)
        {
            string bonedName = "shoulder.R/arm.R";
            if (RightBone != SnapBone.arm)
            {
                bonedName += "/forearm.R";
                if (RightBone != SnapBone.forearm)
                {
                    bonedName += "/hand.R";
                    if (RightBone != SnapBone.hand)
                    {
                        bonedName += "/fingers.R";
                    }
                }
            }
            RightSnap.transform.parent = InBody.GetBone(bonedName);
            RightSnap.transform.localPosition = Vector3.zero;
        }
        InBody.WeaponAnimTrigger = AnimationTrigger;
    }
}
