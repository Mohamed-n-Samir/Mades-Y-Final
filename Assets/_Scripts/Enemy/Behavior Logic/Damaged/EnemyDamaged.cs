using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDamaged", menuName = "Enemy Logic/Damage Logic/Enemy Damaged")]
public class EnemyDamaged : EnemyDamagedSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("Damaged", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("Damaged", false);
        enemy.isTakingDamage = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
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

    public override void OnEnableLogic(){
        base.OnEnableLogic();
        enemy.eventHandler.OnTakingDamageFinish += OnFinishDamaged;
    }

    public override void OnDisableLogic(){
        base.OnDisableLogic();
        enemy.eventHandler.OnTakingDamageFinish -= OnFinishDamaged;

    }

    private void OnFinishDamaged(){
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }
}
