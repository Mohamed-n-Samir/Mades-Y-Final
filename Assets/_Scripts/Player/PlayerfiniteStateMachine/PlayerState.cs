using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected Player player;
    protected PlayerData playerData;
    protected PlayerStateMachine playerStateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName = null)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.Core;
    }

    public virtual void EnterState()
    {
        DoChecks();
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        if (animBoolName != null)
        {
            player.PlayerAnimator.SetBool(animBoolName, true);
        }
    }

    public virtual void ExitState()
    {
        isExitingState = true;
        if (animBoolName != null)
        {
            player.PlayerAnimator.SetBool(animBoolName, false);
        }
    }

    public virtual void FrameUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void LateUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
