using Unity.VisualScripting;
using UnityEngine;

public class Damage : WeaponComponent<DamageData,AttackDamage>
{
    private ActionHitBox hitBox;

    private void HandleDetectCollider2D(Collider2D[] colliders){
        foreach(var item in colliders){
            if(item.TryGetComponent(out IDamageable damageable)){
                damageable.Damage(currentAttackData.Amount);
            }
            else if(item.TryGetComponent(out IBreakable breakable)){
                breakable.Break();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        hitBox = GetComponent<ActionHitBox>();
    }

    protected override void Start()
    {
        base.Start();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}