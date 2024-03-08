using UnityEngine;

public class PlayerIdleState : PlayerNormalState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName = null) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
        Movement.SetVeloityZero();
        player.PlayerAnimator.SetFloat("Speed", 0f);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        if (player.InputHandler.Movement != Vector2.zero)
        {

            player.StateMachine.ChangeState(player.RunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}