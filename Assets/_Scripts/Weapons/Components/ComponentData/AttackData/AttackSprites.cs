using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackSprites : AttackData
{
    [field: SerializeField] public Sprite[] Sprites {get; private set;}    
}