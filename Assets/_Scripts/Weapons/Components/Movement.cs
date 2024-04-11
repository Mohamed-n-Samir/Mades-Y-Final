using UnityEngine;
using CoreSystem;

public class Movement : WeaponComponent<MovementData, AttackMovement>
{

    protected CoreSystem.Movement coreMovement;

    private void HandleStartMovement()
    {
        coreMovement.SetVelocity(currentAttackData.Velocity * new Vector2(Player.PlayerAnimator.GetFloat("Horizontal"), Player.PlayerAnimator.GetFloat("Vertical")));
        // Debug.Log(currentAttackData);
    }

    private void HandleStopMovement()
    {
        coreMovement.SetVeloityZero();
    }

    protected override void Start()
    {
        base.Start();
        coreMovement = Core.GetCoreComponent<CoreSystem.Movement>();

        eventHandler.OnStartMovement += HandleStartMovement;
        eventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        eventHandler.OnStartMovement -= HandleStartMovement;
        eventHandler.OnStopMovement -= HandleStopMovement;
    }

}