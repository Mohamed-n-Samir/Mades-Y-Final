using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerRunState : PlayerNormalState
{

    private Vector2 playerDirection;

    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName = null) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        playerDirection = player.InputHandler.Movement;

    }

    public override void ExitState()
    {
        base.ExitState();
        ProcessInputs(0f);

        // Movement.SetVeloityZero();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        playerDirection = player.InputHandler.Movement;
        if (playerDirection == Vector2.zero)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ProcessInputs(0.001f);

    }

    public override void LateUpdate(){
        Movement.SetVelocity(playerDirection * playerData.MoveSpeed);

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    void ProcessInputs(float Speed)
    {
        if (playerDirection != Vector2.zero)
        {
            player.PlayerAnimator.SetFloat(_horizontal, Mathf.Round(playerDirection.x));
            player.PlayerAnimator.SetFloat(_vertical, Mathf.Round(playerDirection.y));
            player.PlayerAnimator.SetFloat(_speed, Speed);
        }
    }
}
