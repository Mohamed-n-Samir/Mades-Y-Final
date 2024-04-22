using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;
    [SerializeField] private float _chaseCounterRestCooldown = 2f;

    private float _timer;


    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("walking", true);
        enemy.agent.speed = _movementSpeed;
        enemy.agent.isStopped = false;
        _timer = 0;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("walking", false);
        enemy.agent.isStopped = true;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        // Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.agent.SetDestination(playerTransform.position);
        enemy.CheackLeftOrRightFacing(playerTransform.position - enemy.transform.position);
    // new Vector2(enemy.agent.destination.x, enemy.agent.destination.y)

        // enemy.EnemyMove(moveDirection * _movementSpeed);

        if (!enemy.IsAggroad)
        {
            if(_timer > _chaseCounterRestCooldown){
                enemy.StateMachine.ChangeState(enemy.IdleState);
            }
            _timer += Time.deltaTime;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    public override void OnEnableLogic()
    {
    }

    public override void OnDisableLogic()
    {
    }

}
