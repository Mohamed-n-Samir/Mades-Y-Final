using System;
using CoreSystem;
using Unity.VisualScripting;
using UnityEngine;

public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
{
    private ActionHitBox hitBox;

    private CoreSystem.Movement movement;

    private void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockBackable knockBackable))
            {
                Debug.Log("knockback");
                knockBackable.KnockBack(currentAttackData.Strength,movement.FacingDirection);
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<ActionHitBox>();

        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

        movement = Core.GetCoreComponent<CoreSystem.Movement>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}