using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInput : MonoBehaviour
{
    private Player playerInput;

    public bool IsMovingUp { get; private set; }
    public bool IsMovingLeft { get; private set; }
    public bool IsMovingRight { get; private set; }

    public bool IsMovingDown { get; private set; }

    private void OnEnable()
    {
        playerInput = new Player();
        playerInput.Enable();

        playerInput.Movement.Up.performed += OnUpMovePerformed;
        playerInput.Movement.Up.canceled += OnUpMoveCanceled;

        playerInput.Movement.Left.performed += OnLeftMovePerformed;
        playerInput.Movement.Left.canceled += OnLeftMoveCanceled;

        playerInput.Movement.Right.performed += OnRightMovePerformed;
        playerInput.Movement.Right.canceled += OnRightMoveCanceled;

        playerInput.Movement.Down.performed += OnDownMovePerformed;
        playerInput.Movement.Down.canceled += OnDownMoveCanceled;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Movement.Up.performed -= OnUpMovePerformed;
        playerInput.Movement.Up.canceled -= OnUpMoveCanceled;

        playerInput.Movement.Left.performed -= OnLeftMovePerformed;
        playerInput.Movement.Left.canceled -= OnLeftMoveCanceled;

        playerInput.Movement.Right.performed -= OnRightMovePerformed;
        playerInput.Movement.Right.canceled -= OnRightMoveCanceled;

        playerInput.Movement.Down.performed -= OnDownMovePerformed;
        playerInput.Movement.Down.canceled -= OnDownMoveCanceled;
    }

    private void OnUpMovePerformed(CallbackContext context)
    {
        IsMovingUp = true;
    }

    private void OnLeftMovePerformed(CallbackContext context)
    {
        IsMovingLeft = true;
    }

    private void OnRightMovePerformed(CallbackContext context)
    {
        IsMovingRight = true;
    }

    private void OnDownMovePerformed(CallbackContext context)
    {
        IsMovingDown = true;
    }

    private void OnDownMoveCanceled(CallbackContext context)
    {
        IsMovingDown = false;
    }

    private void OnRightMoveCanceled(CallbackContext context)
    {
        IsMovingRight = false;
    }

    private void OnLeftMoveCanceled(CallbackContext context)
    {
        IsMovingLeft = false;
    }

    private void OnUpMoveCanceled(CallbackContext context)
    {
        IsMovingUp = false;
    }
}
