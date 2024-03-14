using UnityEngine;
using CoreSystem;


public class PlayerNormalState : PlayerState
{

    #region Animation Strings

    protected const string _horizontal = "Horizontal";
    protected const string _vertical = "Vertical";
    protected const string _speed = "Speed";

    #endregion



    private bool primaryAttackInput;
    private bool secondaryAttackInput;
    protected CoreSystem.Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private CoreSystem.Movement movement;


    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }


    public PlayerNormalState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    private CollisionSenses collisionSenses;

    private bool isGrounded;
    private bool dashInput;

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
        }
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        dashInput = player.InputHandler.IsDashing;
        primaryAttackInput = player.InputHandler.AttackInputs[(int)InputManager.CombatInputs.primary];
        secondaryAttackInput = player.InputHandler.AttackInputs[(int)InputManager.CombatInputs.secondary];


        if (primaryAttackInput)
        {
            
            playerStateMachine.ChangeState(player.PlayerPrimaryAttackState);
        }
        // else if (secondaryAttackInput)
        // {
        //     playerStateMachine.ChangeState(player.PlayerSecondrayAttackState);
        // }
        if (dashInput && player.InputHandler.Movement != Vector2.zero)
        {
            playerStateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
