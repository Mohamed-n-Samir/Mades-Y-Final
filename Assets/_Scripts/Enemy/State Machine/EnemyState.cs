using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate()
    {
        Debug.Log(enemy.StateMachine.CurrentEnemyState);
        if (enemy.IsAggroad && !enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }
    public virtual void PhysicsUpdate() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
