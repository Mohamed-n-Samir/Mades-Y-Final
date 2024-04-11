using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Stand Still", menuName = "Enemy Logic/Idle Logic/Stand Still")]
public class EnemyIdleStandStill : EnemyIdleSOBase
{
    public float _timeToStandStill = 2f;
    public float _exitTime; 

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("walking", false);
        _exitTime =0f;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("not in idel");
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.EnemyMove(Vector2.zero);
        if(_exitTime > _timeToStandStill){
            enemy.StateMachine.ChangeState(enemy.WanderState);
        }
        else {
            _exitTime += Time.deltaTime;
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

    public override void OnEnableLogic() { 
        Debug.Log("a7a");
    }
    public override void OnDisableLogic() { }
}
