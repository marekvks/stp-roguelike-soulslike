using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        // This will be changed when we change to the unity's new input system
        _animator.SetFloat("direction magnitude", _playerMovement.direction.magnitude);
    }
}
