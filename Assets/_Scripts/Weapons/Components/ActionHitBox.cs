using System;
using UnityEngine;

public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
{
    public event Action<Collider2D[]> OnDetectedCollider2D;

    public Vector2 PlayerDirection { get; private set; }
    private Vector2 offset;
    private Collider2D[] detected;

    private void HandleAttackAction()
    {
        // Debug.Log(Player);
        PlayerDirection = new Vector2(Player.PlayerAnimator.GetFloat("Horizontal"), Player.PlayerAnimator.GetFloat("Vertical"));
        offset = CalculateOffset(PlayerDirection);
        detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);



        if (detected.Length == 0)
            return;

        OnDetectedCollider2D?.Invoke(detected);

    }

    protected override void Start()
    {
        base.Start();
        eventHandler.OnAttackAction += HandleAttackAction;


    }

    protected override void OnDestroy()
    {
        eventHandler.OnAttackAction -= HandleAttackAction;

    }
    private void OnDrawGizmosSelected()
    {
        if (data == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        foreach (var item in data.AttackData)
        {
            if (!item.Debug)
            {
                continue;
            }
            Gizmos.DrawWireCube(offset, item.HitBox.size);
        }
    }

    private Vector3 CalculateOffset(Vector2 playerDirection)
    {
        // Calculate the offset based on the player's direction

        float offsetX = transform.position.x + playerDirection.x * currentAttackData.HitBox.center.x;
        float offsetY = transform.position.y + playerDirection.y * currentAttackData.HitBox.center.y;

        // Debug.Log(playerDirection.y);

        if(playerDirection.y == 0){
            offsetY = transform.position.y + currentAttackData.HitBox.center.y / 2 - 0.25f;
        }
        else if(playerDirection.y == -1){
            offsetY += .3f;
        }

        return new Vector3(offsetX, offsetY, transform.position.z);
    }
}