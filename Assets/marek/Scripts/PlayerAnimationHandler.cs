using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(PlayerMovement))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() => _animator.SetFloat("Speed", _playerMovement.CurrentSpeed);

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