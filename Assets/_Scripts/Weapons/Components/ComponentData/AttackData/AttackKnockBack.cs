using System;
using UnityEngine;

[Serializable]
public class AttackKnockBack : AttackData
{
    [field: SerializeField] public float Strength {get; private set;}
}