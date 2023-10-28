using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterController;

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    private float _currentSpeed = 0f;
    [SerializeField] private float _characterRotationSmoothness = 0.1f;

    public Vector3 direction;

    private float yVelocity;

    private void Awake()
    {
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        // This will be changed when we change to the unity's new input system
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0, z);
        direction.Normalize();

        // fixes rotation bugs
        if (direction.magnitude <= 0.1f) return;

        // Calculating the angle where should the character rotate according to the movement
        float yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation, ref yVelocity, _characterRotationSmoothness);
        transform.rotation = Quaternion.Euler(0f, smoothRotation, 0f);

        _characterController.Move(direction * _currentSpeed * Time.deltaTime);
    }
}
