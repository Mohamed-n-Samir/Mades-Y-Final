using UnityEngine;
using CoreSystem;
using System;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerPrimaryAttackState PlayerPrimaryAttackState { get; private set; }
    // public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public InputManager InputHandler { get; private set; }
    public Rigidbody2D PlayerRB { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    // public Stats Stats { get; private set; }
    
    #endregion

    #region Other Variables         

    private Weapon primaryWeapon;
    // private Weapon secondaryWeapon;
    
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        Core = GetComponentInChildren<Core>();
        primaryWeapon.SetPlayer(this);
        primaryWeapon.SetCore(Core);

        // Stats = Core.GetCoreComponent<Stats>();
        
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData);
        RunState =  new PlayerRunState(this, StateMachine,playerData);
        DashState = new PlayerDashState(this, StateMachine,playerData);
        PlayerPrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, playerData,"Attack", primaryWeapon);
        // SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", secondaryWeapon, CombatInputs.secondary);
    }

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();

        InputHandler = GetComponent<InputManager>();
        PlayerRB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        StateMachine.CurrentPlayerState.LateUpdate();
    }

    public void Damage(int damage){

    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentPlayerState.AnimationTrigger();
    private void AnimtionFinishTrigger() => StateMachine.CurrentPlayerState.AnimationFinishTrigger();
    #endregion
}