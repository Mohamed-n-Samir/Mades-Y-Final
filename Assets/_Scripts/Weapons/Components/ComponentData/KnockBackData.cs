using UnityEngine;

public class KnockBackData : ComponentData<AttackKnockBack>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(KnockBack);
    }
}