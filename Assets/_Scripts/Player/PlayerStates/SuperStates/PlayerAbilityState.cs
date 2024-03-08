using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    public bool IsAbilityDone { get; set; }

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    protected Vector2 playerLastDirection;

    private bool isGrounded;


    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
        }
    }

    public override void EnterState()
    {
        base.EnterState();

        IsAbilityDone = false;
        playerLastDirection = player.InputHandler.Movement;
    }

    public override void ExitState()
    {
        base.ExitState();

        IsAbilityDone =false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (IsAbilityDone)
        {
            // if (PlayerDirection == Vector2.zero && isGrounded)
            if (player.InputHandler.Movement == Vector2.zero)
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
            else
            {
                playerStateMachine.ChangeState(player.RunState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // if(Movement.CurrentVelocity != Vector2.zero){
        //     playerLastDirection = PlayerDirection;
        // }
    }
}
