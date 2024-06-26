using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable, IKnockBackable
{
    public bool IsFacingLeft { get; set; } = true;
    public Rigidbody2D EnemyRB { get; set; }
    public bool IsAggroad { get; set; } = false;
    public bool IsWithinStrikingDistance { get; set; } = false;
    public FloatingHealthBar floatingHealthBar;
    public Animator animator;
    public NavMeshAgent agent;
    public bool isTakingDamage = false;

    [field: SerializeField] public float MaxKnockBackTime { get; set; } = 0.2f;
    public bool IsKnockBackActive { get; set; }
    public float KnockBackStartTime { get; set; }

    #region Health variables
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    #endregion

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyWanderState WanderState { get; set; }
    public EnemyDamagedState DamagedState { get; set; }
    // public EnemyAttackState AttackState { get; set; }


    #endregion

    #region Animations 

    public EnemyAnimationEventHandler eventHandler;

    #endregion

    #region ScriptableObject Variables

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyWanderSOBase EnemyWanderBase;
    [SerializeField] private EnemyDamagedSOBase EnemyDamagedBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyWanderSOBase EnemyWanderBaseInstance { get; set; }
    public EnemyDamagedSOBase EnemyDamagedBaseInstance { get; set; }



    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        eventHandler = GetComponent<EnemyAnimationEventHandler>();

        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyWanderBaseInstance = Instantiate(EnemyWanderBase);
        EnemyDamagedBaseInstance = Instantiate(EnemyDamagedBase);

        StateMachine = new EnemyStateMachine();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyWanderBaseInstance.Initialize(gameObject, this);
        EnemyDamagedBaseInstance.Initialize(gameObject, this);

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        WanderState = new EnemyWanderState(this, StateMachine);
        DamagedState = new EnemyDamagedState(this, StateMachine);

        StateMachine.Initialize(WanderState);


    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        EnemyRB = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        // EnemyMove(new Vector2(0, -1));      
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    // private void Exit(){
    //     StateMachine.ChangeState(IdleState);
    // }

    private void OnEnable()
    {
        IdleState.OnEnable();
        ChaseState.OnEnable();
        WanderState.OnEnable();
        AttackState.OnEnable();
        DamagedState.OnEnable();
        eventHandler.OnDie += Die;
    }

    private void OnDisable()
    {
        IdleState.OnDisable();
        ChaseState.OnDisable();
        WanderState.OnDisable();
        AttackState.OnDisable();
        DamagedState.OnDisable();
        eventHandler.OnDie -= Die;
    }

    // private void OnDrawGizmos()
    // {
    //     StateMachine.CurrentEnemyState.OnDrawGizmos();
    // }

    #region Health / Die Functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        floatingHealthBar.UpdateHealthBar(CurrentHealth, MaxHealth);

        isTakingDamage = true;
        StateMachine.ChangeState(DamagedState);

        if (CurrentHealth <= 0)
        {
            animator.SetBool("die", true);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Movement Functions
    public void EnemyMove(Vector2 velocity)
    {
        EnemyRB.velocity = velocity;
        CheackLeftOrRightFacing(velocity);
    }


    public void CheackLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingLeft && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingLeft = !IsFacingLeft;
        }
        else if (!IsFacingLeft && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingLeft = !IsFacingLeft;
        }
    }

    public void CheackLeftOrRightFacingAgent(Vector2 velocity)
    {
        if (IsFacingLeft && agent.speed > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingLeft = !IsFacingLeft;
        }
        else if (!IsFacingLeft && agent.speed < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingLeft = !IsFacingLeft;
        }
    }
    #endregion

    #region Animation Triggers

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    #endregion

    #region Distance Check

    public void SetAggroStatus(bool isAggroad)
    {
        IsAggroad = isAggroad;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    public void KnockBack(float strength, Vector2 direction)
    {
        EnemyRB.isKinematic = false;
        Vector2 difference = direction.normalized * strength;
        EnemyRB.AddForce(difference, ForceMode2D.Impulse);
        EnemyRB.isKinematic = true;
        // IsKnockBackActive = true;
        // KnockBackStartTime = Time.time;
    }

    public void CheckKnockBack()
    {
        // if (IsKnockBackActive && Time.time >= KnockBackStartTime + MaxKnockBackTime)
        // {
        //     IsKnockBackActive = false;
        // }
    }

    #endregion


}
