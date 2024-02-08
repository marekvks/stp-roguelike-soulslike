using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class InputHandler : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;

    public delegate void InteractFunction();

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Movement.Enable();
    }

    #region Movement

    /// <returns>(Vector3) Normalized movement input</returns>
    public Vector3 GetMovement()
    {
        Vector2 input = _playerInputAction.Movement.Move.ReadValue<Vector2>();
        Vector3 input3D = new Vector3(input.x, 0f, input.y).normalized;
        return input3D;
    }

    /// <summary>
    /// Subscribes two functions to run input
    /// </summary>
    /// <param name="start">Function, which is called when run button is pushed</param>
    /// <param name="end">Function, which is called when run button is released</param>
    public void SubscribeToRun(Action<InputAction.CallbackContext> start, Action<InputAction.CallbackContext> end)
    {
        _playerInputAction.Movement.Run.performed += start;
        _playerInputAction.Movement.Run.canceled += end;
    }

    /// <summary>
    /// Unsubscribes two functions from run input
    /// </summary>
    /// <param name="start">Function, which is called when run button is pushed</param>
    /// <param name="end">Function, which is called when run button is released</param>
    public void UnsubscribeFromRun(Action<InputAction.CallbackContext> start, Action<InputAction.CallbackContext> end)
    {
        _playerInputAction.Movement.Run.performed -= start;
        _playerInputAction.Movement.Run.canceled -= end;
    }

    public void SubscribeToLockOnTarget(Action<InputAction.CallbackContext> function)
    {
        _playerInputAction.Movement.LockOnTarget.performed += function;
    }

    public void UnsubscribeFromLockOnTarget(Action<InputAction.CallbackContext> function)
    {
        _playerInputAction.Movement.LockOnTarget.performed -= function;
    }

    #endregion

    #region Interaction

        public void SubscribeToInteraction(Action<InputAction.CallbackContext> InteractFunction)
        {
            _playerInputAction.Movement.Interact.performed += InteractFunction;
        }

        public void UnsubscribeFromInteraction(Action<InputAction.CallbackContext> InteractFunction)
        {
            _playerInputAction.Movement.Interact.performed -= InteractFunction;
        }

    #endregion
}