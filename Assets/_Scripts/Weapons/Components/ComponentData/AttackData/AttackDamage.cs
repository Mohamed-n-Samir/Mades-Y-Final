using System;
using UnityEngine;

[Serializable]
public class AttackDamage : AttackData
{
    [field: SerializeField] public float Amount {get; private set;}
}