using UnityEngine;
using System;

[Serializable]
public class AttackAnimations : AttackData
{
    [field: SerializeField] public AnimationClip[] Animations {get; private set;}    
}


