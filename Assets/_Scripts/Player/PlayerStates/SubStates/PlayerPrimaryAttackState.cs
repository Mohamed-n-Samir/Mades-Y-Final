using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState
{

    private Weapon weapon;

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData,string animBoolName, Weapon weapon) : base(player, playerStateMachine, playerData,animBoolName)
    {
        this.weapon = weapon;
        weapon.OnExit += ExitHandler;
    }

    public override void EnterState()
    {
        base.EnterState();
        // player.PrimaryAttackBaseInstance.DoEnterLogic();
        player.PlayerAnimator.SetBool("Attack", true);
        // player.PlayerMove(Vector2.zero);
        weapon.Enter();
    }

    public override void ExitState()
    {
        base.ExitState();
        // player.PrimaryAttackBaseInstance.DoExitLogic();
        player.PlayerAnimator.SetBool("Attack", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // player.PrimaryAttackBaseInstance.DoFrameUpdateLogic();
        if (isAnimationFinished)
        {
            IsAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // player.PrimaryAttackBaseInstance.DoPhysicsLogic();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);

    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();

    }
}
