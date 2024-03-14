using System;
using UnityEngine;

[Serializable]
public class AttackKnockBack : AttackData
{
    [field: SerializeField] public Vector2 Angle {get; private set;}
    [field: SerializeField] public float Strength {get; private set;}
}