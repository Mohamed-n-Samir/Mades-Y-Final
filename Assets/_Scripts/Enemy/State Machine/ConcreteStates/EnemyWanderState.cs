using UnityEngine;

public class EnemyWanderState : EnemyState
{
    public EnemyWanderState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyWanderBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyWanderBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyWanderBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyWanderBaseInstance.DoPhysicsLogic();
    }

    public override void OnEnable()
    {
        enemy.EnemyWanderBaseInstance.OnEnableLogic();
    }

    public override void OnDisable()
    {
        enemy.EnemyWanderBaseInstance.OnEnableLogic();
    }

    public override void OnDrawGizmos(){
        enemy.EnemyWanderBaseInstance.OnDrawGizmos();
    }

}