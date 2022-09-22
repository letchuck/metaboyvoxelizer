using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    public string TriggerNamer = "";

    
    public void HandleClick()
    {
        Animator MetaAnimator = GameObject.FindObjectOfType<Animator>();
        if(MetaAnimator != null && TriggerNamer != "")
        {
            if(TriggerNamer == "Attack")
            {
                WeaponComponent MetaWeapon = FindObjectOfType<WeaponComponent>();
                if(MetaWeapon && MetaWeapon.AnimationTrigger != "")
                {
                    MetaAnimator.SetTrigger(MetaWeapon.AnimationTrigger);
                }
                else
                {
                    MetaAnimator.SetTrigger("AttackPunch");
                }
            }
            else
            {
                MetaAnimator.SetTrigger(TriggerNamer);
            }            
        }
    }
}
