using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")] 
    //[SerializeField] private CharacterController _characterController;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    [SerializeField] private PlayerMovement _playerMovement;

    [Header("Movement Settings")] 
    [SerializeField] private float _minimumActiveSpeed = 2f;

    [Header("Player Flags")] 
    //private bool _isRunning; // Only will be implemented if running attacks are going to be considered, which they so far aren't. (Perhaps QoL)
    private bool _canAttack;

    public bool Hittable = true;

    private Vector3 _movementDirection;
    
    public int LightComboCounter = 0;
    public int HeavyComboCounter = 0;
    public int MaxLightComboCount = 2;
    public int MaxHeavyComboCount = 2;
    
    public int ComboDelay = 3;
    public bool LightAtkFlag = false;
    public bool CanAttack
    {
        get { return _canAttack; }
    }
    void Start()
    {
        SubscribeToCombatActions();
        EnableAttacking();
    }

    private void SubscribeToCombatActions()
    {
        _inputHandler.SubscribeToBackstep(Backstep);
        _inputHandler.SubscribeToRoll(Roll);
        _inputHandler.SubscribeToLightAttack(LightAttack);
        _inputHandler.SubscribeToHeavyAttack(HeavyAttack);
    }

    private void UnsubscribeToCombatActions()
    {
        _inputHandler.UnsubscribeFromBackstep(Backstep);
        _inputHandler.UnsubscribeFromRoll(Roll);
        _inputHandler.UnsubscribeFromLightAttack(LightAttack);
        _inputHandler.UnsubscribeFromHeavyAttack(HeavyAttack);
    }
    void OnDestroy()
    {
        UnsubscribeToCombatActions();
        DisableAttacking();
    }
    /// <summary>
    /// For both light and heavy attack functions:
    /// Ensures that the player isn't attacking (is in a cutscene, is already attacking, is rolling...)
    /// For combo system damage ensure that light attack is the one being clicked OR if false it's therefore the heavy attack.
    /// </summary>
    private void LightAttack(InputAction.CallbackContext context)
    {
        if (!_canAttack) return;

        LightAtkFlag = true;
        _animationHandler.PerformLightAttack();
    }

    private void HeavyAttack(InputAction.CallbackContext context)
    {
        if (!_canAttack) return;
        _animationHandler.PerformHeavyAttack();
    }
    /// <summary>
    /// Checks whether player can move and whether to decide between Roll or Backstep, Roll is when player is ACTIVELY moving, so if the player lets go of
    /// their keys and press SPACE, then Backstep is selected as the CurrentSpeed of the player goes below the minimumActiveSpeed.
    /// </summary>

    private void Roll(InputAction.CallbackContext context)
    {
        if (_playerMovement.DisableMovement) return;
        if (_playerMovement.CurrentSpeed < _minimumActiveSpeed) return;
        _animationHandler.PerformRoll();
    }

    private void Backstep(InputAction.CallbackContext context)
    {
        if (_playerMovement.DisableMovement) return;
        if (_playerMovement.CurrentSpeed >= _minimumActiveSpeed) return;
        _animationHandler.PerformBackstep();

    }
    
    // For use in cutscenes, quick turns, etc..
    public void DisableAttacking() => _canAttack = false;
    public void EnableAttacking() => _canAttack = true;
}
