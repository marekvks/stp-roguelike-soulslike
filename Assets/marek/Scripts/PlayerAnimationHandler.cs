using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(PlayerMovement), typeof(PlayerCombat))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCombat _playerCombat;
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

    private System.DateTime _animationEnd;

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

    
    //Combat & Combat Movement
    
    /// <summary>
    /// For both Light and Heavy attack functions:
    /// Checks if time passed since last attack animation is larger than the combo delay or if the combo count precedes the max combo count, if so, reset combo count to 0.
    /// Checks if attack isn't already triggered.
    /// Removes ability to attack and enables animation of a certain light attack dependent on the combo.
    /// </summary>
    public void PerformLightAttack()
    {
        if((System.DateTime.UtcNow - _animationEnd).Seconds > _playerCombat.ComboDelay || _playerCombat.LightComboCounter > _playerCombat.MaxLightComboCount) _playerCombat.LightComboCounter = 0;

        if (_animator.GetBool("LightAttack" + _playerCombat.LightComboCounter)) return;
        _playerCombat.DisableAttacking();
        _animator.SetBool("LightAttack" + _playerCombat.LightComboCounter, true);
    }

    public void EndLightAttack()
    {
        _playerCombat.EnableAttacking();
        _animator.SetBool("LightAttack" + _playerCombat.LightComboCounter, false);
        
        _playerCombat.LightComboCounter++;
        _playerCombat.LightAtkFlag = false;
        _animationEnd = System.DateTime.UtcNow;
    }
    

    public void PerformHeavyAttack()
    {
        if((System.DateTime.UtcNow - _animationEnd).Seconds > _playerCombat.ComboDelay || _playerCombat.HeavyComboCounter > _playerCombat.MaxHeavyComboCount) _playerCombat.HeavyComboCounter = 0;
        
        if (_animator.GetBool("HeavyAttack"  + _playerCombat.HeavyComboCounter)) return;
        _playerCombat.DisableAttacking();
        _animator.SetBool("HeavyAttack"  + _playerCombat.HeavyComboCounter, true);
        
    }

    public void EndHeavyAttack()
    {
        _playerCombat.EnableAttacking();
        _animator.SetBool("HeavyAttack"  + _playerCombat.HeavyComboCounter, false);

        _playerCombat.HeavyComboCounter++;
        _animationEnd = System.DateTime.UtcNow;
    }
    /// <summary>
    /// Checks whether Roll/Backstep isn't already active.
    /// Removes the option to move/attack and the player cannot be hit.
    /// </summary>
    public void PerformRoll()
    {
        if (_animator.GetBool("Roll")) return;
        _playerCombat.Hittable = false;
        _playerMovement.DisableMove();
        _playerCombat.DisableAttacking();
        _animator.SetBool("Roll", true);
    }

    public void EndRoll()
    {
        _playerCombat.Hittable = true;
        _playerCombat.EnableAttacking();
        _playerMovement.EnableMove();
        _animator.SetBool("Roll", false);
    }

    public void PerformBackstep()
    {
        if (_animator.GetBool("Backstep")) return;
        _playerCombat.Hittable = false;
        _playerMovement.DisableMove();
        _playerCombat.DisableAttacking();
        _animator.SetBool("Backstep", true);
    }

    public void EndBackstep()
    {
        _playerCombat.Hittable = true;
        _playerCombat.EnableAttacking();
        _playerMovement.EnableMove();
        _animator.SetBool("Backstep", false);
    }
    
    // End of Combat & Combat Movement
    
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