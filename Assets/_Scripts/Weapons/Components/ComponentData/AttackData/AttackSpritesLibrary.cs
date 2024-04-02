using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

[Serializable]
public class AttackSpritesLibrary : AttackData
{
    [field: SerializeField] public SpriteLibraryAsset SpriteLibraryAsset {get; private set;}    
    [field: SerializeField] public AnimationClip Animations {get; private set;}    
}