using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Straight-Single Projectile", menuName = "Enemy Logic/Attack Logic/Straight Single Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{

    [SerializeField] private Rigidbody2D projectilePrefab;
    [SerializeField] private float _timeBetweenShots = 2f;
    [SerializeField] private float _timeTillExit = 2f;
    [SerializeField] private float _distanceToCountExit = 1f;
    [SerializeField] private float _projectileSpeed = 10f;
    private Vector2 dir;

    private float _timer;
    private float _exitTimer;


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _exitTimer = _timeTillExit;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.animator.SetBool("swordThrough", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.EnemyMove(Vector2.zero);

        if (enemy.transform.position.x > playerTransform.position.x)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }

        if (_timer > _timeBetweenShots)
        {
            enemy.animator.SetBool("swordThrough", true);
            _timer = 0f;

            dir = (playerTransform.position - enemy.transform.position).normalized;

        }

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit + 7)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;
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

    private void ThroughProjectile()
    {
        // Debug.Log("test");
        Vector3 throughPoint = enemy.transform.GetChild(1).position;
        Rigidbody2D projectile = GameObject.Instantiate(projectilePrefab, throughPoint, Quaternion.identity);
        projectile.velocity = dir * _projectileSpeed;
    }

    private void OnFinishAttack()
    {
        enemy.animator.SetBool("swordThrough", false);
    }

    public override void OnEnableLogic()
    {
        enemy.eventHandler.OnAttack += ThroughProjectile;
        enemy.eventHandler.OnFinish += OnFinishAttack;
    }

    public override void OnDisableLogic()
    {
        enemy.eventHandler.OnAttack -= ThroughProjectile;
        enemy.eventHandler.OnFinish -= OnFinishAttack;
    }
}
