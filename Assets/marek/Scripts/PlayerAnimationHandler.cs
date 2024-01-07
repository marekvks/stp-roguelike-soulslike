using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(PlayerMovement))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private LockOn _lockOn;

    [Header("Values")]
    private StrafeState _currentStrafeState;

    enum StrafeState
    {
        idle,
        forward,
        right,
        left,
        backwards
    }

    private void FixedUpdate()
    {
        _animator.SetBool("LockOn", _lockOn.Locked);
        if (_lockOn.Locked)
            Strafe();
        else
            Move();
    }

    private void Move() => _animator.SetFloat("Speed", _playerMovement.CurrentSpeed);

    private void Strafe()
    {
        Vector3 input = _inputHandler.GetMovement();

        int x = Mathf.RoundToInt(input.x);
        int z = Mathf.RoundToInt(input.z);

        switch (x, z)
        {
            case (1, 0):
                _currentStrafeState = StrafeState.right;
                break;
            case (-1, 0):
                _currentStrafeState = StrafeState.left;
                break;
            case (0, 1):
                _currentStrafeState = StrafeState.forward;
                break;
            case (0, -1):
                _currentStrafeState = StrafeState.backwards;
                break;
            case (1, 1):
            case (1, -1):
                _currentStrafeState = StrafeState.right;
                break;
            case (-1, 1):
            case (-1, -1):
                _currentStrafeState = StrafeState.left;
                break;
            default:
                _currentStrafeState = StrafeState.idle;
                break;
        }

        _animator.SetInteger("LockOnStrafe", (int)_currentStrafeState);
    }

    /// <summary>
    /// Performs a quickturn animation
    /// </summary>
    public void PerformQuickTurn()
    {
        if (_animator.GetBool("Quickturn")) return;
        _playerMovement.DisableMove();
        _animator.SetBool("Quickturn", true);
    }

    /// <summary>
    /// This function is hooked to a "Quickturn" animation
    /// </summary>
    public void EndQuickTurn()
    {
        _playerMovement.EnableMove();
        _animator.SetBool("Quickturn", false);
    }
}