using System;
using CoreSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponDataSO Data{get; private set;}

    public event Action OnEnter;
    public event Action OnExit;

    [SerializeField] private float attackCounterResetCooldown = 1f;


    private int currentAttackCounter = 0;

    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
    }


    public Animator BaseAnimator {get; private set;}
    public GameObject BaseGameObject {get; private set;}
    public GameObject WeaponSpriteGameObject {get; private set;} 

    protected PlayerPrimaryAttackState primaryState;
    // protected PlayerSecondaryAttackState secondaryState;

    public WeaponsAnimationEventHandler EventHandler {get; private set;}

    public Player Player{get; private set;}

    public Core Core;

    public Vector2 AttackDirection { get; set; }


    private Timer attackCounterResetTimer;


    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
        BaseAnimator = BaseGameObject.GetComponent<Animator>();
        EventHandler = BaseGameObject.GetComponent<WeaponsAnimationEventHandler>();

        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    private void Update()
    {
        attackCounterResetTimer.Tick();
    }


    public virtual void Enter()
    {

        attackCounterResetTimer.StopTimer();

        AttackDirection = new Vector2(Mathf.Round(Player.PlayerAnimator.GetFloat("Horizontal")), Mathf.Round(Player.PlayerAnimator.GetFloat("Vertical")));
        
        Debug.Log(AttackDirection);
        BaseAnimator.SetBool("Attack", true);
        BaseAnimator.SetFloat("Vertical", AttackDirection.y);
        BaseAnimator.SetFloat("Horizontal", AttackDirection.x);
        BaseAnimator.SetInteger("Counter", CurrentAttackCounter);

        OnEnter?.Invoke();

    }

    private void Exit()
    {
        // Debug.Log(CurrentAttackCounter);
        BaseAnimator.SetBool("Attack", false);
        BaseAnimator.SetFloat("Vertical", 0f);
        BaseAnimator.SetFloat("Horizontal", 0f);
        attackCounterResetTimer.StartTimer();
        OnExit?.Invoke();
        CurrentAttackCounter++;
    }


    public void InitializeWeapon(PlayerPrimaryAttackState state)
    {
        this.primaryState = state;
    }

    // public void InitializeWeapon2(PlayerSecondaryAttackState state)
    // {
    //     this.secondaryState = state;
    // }

    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.onTimerDone += ResetAttackCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.onTimerDone -= ResetAttackCounter;

    }

    public void SetPlayer(Player player){
        Player = player;
    }

    public void SetCore(Core core){
        Core = core;
    }

    private void ResetAttackCounter() => CurrentAttackCounter = 0;

    public void SetData(WeaponDataSO data){
        Data = data;
    }


}
