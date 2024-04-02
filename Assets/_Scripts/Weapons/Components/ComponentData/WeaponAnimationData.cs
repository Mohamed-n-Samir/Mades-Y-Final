using UnityEngine;

public class WeaponAnimationData : ComponentData<AttackAnimations>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponAnimation);
    }
}