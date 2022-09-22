using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TraitEffect
{
    [SerializeField] public AffectsType Affects;
    [SerializeField] public int Value;
}

[Serializable]
public enum AffectsType
{
    Attribute,
    ActionCount,
    WalkCount,
    Weapon
}

[Serializable]
[CreateAssetMenu(fileName = "Trait", menuName = "Thadeus' Last Stand/Trait", order = 1)]
public class TraitObject : ScriptableObject
{
    [SerializeField] public string TraitName = "Name";
    [SerializeField] public string TraitDescription = "Describe it. May automate it later.";
    [SerializeField] public List<TraitEffect> Effects = new List<TraitEffect>();
}
