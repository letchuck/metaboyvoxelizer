using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableComponent : MonoBehaviour
{
    public string WearableName = "Name";
    public WearableType WearType = WearableType.None;
    public SkeletonSkin SkelSkin;

    private void OnValidate()
    {
        if(SkelSkin == null)
        {
            SkelSkin = GetComponentInChildren<SkeletonSkin>();
        }
    }
}