using UnityEngine;

public class WeaponSpriteLibraryData : ComponentData<AttackSpritesLibrary>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponSpriteLibrary);
    }
    
}