using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;
    public Vector2 Movement;
    public bool IsDashing { get; private set; } = false;
    public static bool IsInteracting { get; private set; } = false;
    public bool[] AttackInputs { get; private set; }

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    public enum CombatInputs
    {
        primary,
        secondary
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnMoving(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnDashing(InputAction.CallbackContext context)
    {
        if (context.started && Movement != Vector2.zero)
        {
            IsDashing = true;
        }
    }

    public void OnDashCancel() => IsDashing = false;

    public static void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsInteracting = true;
        }
    }

    public static void OnInteractingCancel() => IsInteracting = false;

    public static void DeactivatePlayerControls()
    {
        playerInput.currentActionMap.Disable();
    }

    public static void ActivatePlayerControls()
    {
        playerInput.currentActionMap.Enable();
    }

}


// public bool[] AttackInputs { get; private set; }

// private void Start()
// {
//     int count = Enum.GetValues(typeof(CombatInputs)).Length;
//     AttackInputs = new bool[count];
// }

// public enum CombatInputs
// {
//     primary,
//     secondary
// }

// public void OnPrimaryAttackInput(InputAction.CallbackContext context)
// {
//     if (context.started)
//     {
//         AttackInputs[(int)CombatInputs.primary] = true;
//     }

//     if (context.canceled)
//     {
//         AttackInputs[(int)CombatInputs.primary] = false;
//     }
// }

// public void OnSecondaryAttackInput(InputAction.CallbackContext context)
// {
//     if (context.started)
//     {
//         AttackInputs[(int)CombatInputs.secondary] = true;
//     }

//     if (context.canceled)
//     {
//         AttackInputs[(int)CombatInputs.secondary] = false;
//     }
// }
