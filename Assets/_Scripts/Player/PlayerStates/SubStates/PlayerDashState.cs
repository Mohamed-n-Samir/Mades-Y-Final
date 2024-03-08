using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public Vector2 DashDirection { get; private set; }
    public float DashSpeed { get; private set; }

    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName = null) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        DashDirection = player.InputHandler.Movement;
        DashSpeed = playerData.dashSpeed;
        player.PlayerAnimator.SetBool("Dash", true);
    }

    public override void ExitState()
    {
        base.ExitState();

        player.PlayerAnimator.SetBool("Dash", false);
        player.InputHandler.OnDashCancel();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        DashSpeed -= DashSpeed * playerData.dashSpeedDrop * Time.deltaTime;


        if (DashSpeed < playerData.dashSpeedMinimum)
        {
            IsAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Debug.Log(DashDirection * DashSpeed);
        Movement.SetVelocity(DashDirection * DashSpeed);

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }


}



