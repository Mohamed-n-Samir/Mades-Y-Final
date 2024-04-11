using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Random Wander", menuName = "Enemy Logic/Wander Logic/Random Wander")]
public class EnemyRandomWander : EnemyWanderSOBase
{
    [field: SerializeField] private float RandomMovementRange = 5f;
    [field: SerializeField] private float RandomMovementSpeed = 1f;

    [field: SerializeField] private float _timer = 3f;
    [field: SerializeField] private float _exitTimer = 0f;

    private Vector3 _targetPos;
    private Vector3 _direction;




    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetBool("walking", true);
        _targetPos = GetRandomPointInCircle();

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("walking", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _direction = (_targetPos - enemy.transform.position).normalized;
        enemy.EnemyMove(_direction * RandomMovementSpeed);

        if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f || canSeeEnviroment(2f))
        {
            // _targetPos = GetRandomPointInCircle();
            enemy.StateMachine.ChangeState(enemy.IdleState);
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

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }

    public override void OnEnableLogic()
    {
        Debug.Log("a7a");
    }
    public override void OnDisableLogic() { }

    private bool canSeeEnviroment(float distance)
    {
        float castDistance = distance;

        Vector3 xEndPoint = enemy.transform.GetChild(1).position + Vector3.right * distance;
        Vector3 yEndPoint = enemy.transform.GetChild(1).position + Vector3.up * distance;

        RaycastHit2D hitX = Physics2D.Linecast(enemy.transform.position, xEndPoint, 1 << LayerMask.NameToLayer("Env"));
        RaycastHit2D hitY = Physics2D.Linecast(enemy.transform.position, yEndPoint, 1 << LayerMask.NameToLayer("Env"));

        if (hitX.collider != null)
        {
            if (hitX.collider.gameObject.CompareTag("Colliders"))
            {
                return true;
            }
        }
        else if (hitY.collider != null)
        {
            if (hitY.collider.gameObject.CompareTag("Colliders"))
            {
                return true;
            }
        }

        return false;
    }
}
