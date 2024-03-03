using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    [SerializeField] private LockOn _lockOn;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _quickTurnPoint;

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _strafeSpeed = 2f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _speedTransitionRate = 0.1f;
    [SerializeField] private float _characterRotationSmoothness = 0.1f;
    private float _currentSpeed;
    private float _yVelocity;

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
    }

    [Header("Player Flags")]
    public bool DisableMovement;
    private bool _run;

    [Header("Gravity Settings")] 
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    private Vector3 _gravityVector = Vector3.zero; // Used for pushing player down according to gravity
    private bool _grounded => Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer); // Ground check

    private void Start()
    {
        SubscribeToInput();
    }

    private void OnDestroy() => UnsubscribeFromInput();

    private void SubscribeToInput() => _inputHandler.SubscribeToRun(Run, Walk);

    private void UnsubscribeFromInput() => _inputHandler.UnsubscribeFromRun(Run, Walk);

    private void Update()
    {
        _quickTurnPoint.position = _camera.position;
        Gravity();
        Move();
    }

    /// <summary>
    ///  Handles gravity
    /// </summary>
    private void Gravity()
    {
        _gravityVector.y += _gravity * Time.deltaTime;
        if (_grounded)
            _gravityVector.y = -2f;
        _characterController.Move(_gravityVector * Time.deltaTime);
    }

    // For debug purposes
    public Vector3 DIRECTION = Vector3.zero;

    /// <summary>
    /// Handles character movement
    /// </summary>
    private void Move()
    {
        Vector3 direction = _inputHandler.GetMovement();

        if (direction.magnitude <= 0.1f)
        {
            // if player is not moving, current speed should be 0
            if (_currentSpeed > 0.1f)
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, Time.deltaTime * _speedTransitionRate);
        }

        if (DisableMovement) return;

        if (_lockOn.Locked)
            Strafe(direction);
        else
            FreeMove(direction);
    }

    private void FreeMove(Vector3 direction)
    {
        // Fixes rotation bugs
        if (direction.magnitude <= 0.1f) return;


        // This is necessary for quickturn to work correctly
        Vector3 localDirection = _quickTurnPoint.right * direction.x + _quickTurnPoint.forward * direction.z;
        // For debug purposes
        DIRECTION = localDirection;

        // Run logic
        _currentSpeed = _run ? Mathf.MoveTowards(_currentSpeed, _runSpeed, Time.deltaTime * _speedTransitionRate) : Mathf.MoveTowards(_currentSpeed, _walkSpeed, Time.deltaTime * _speedTransitionRate);

        // Determines if the player input and position vectors are the exact opposite (with small tolerance)
        // -0.9f is a fixed value and should not be changed
        if (_currentSpeed > _walkSpeed && Vector3.Dot(transform.forward, localDirection) <= -0.9f)
        {
            QuickTurnAnim();
            return;
        }

        // Calculating the correct angle where should the character rotate according to the movement
        float yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
        float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation, ref _yVelocity, _characterRotationSmoothness);
        transform.rotation = Quaternion.Euler(0f, smoothRotation, 0f);

        _characterController.Move(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void Strafe(Vector3 inputDirection)
    {
        Transform target = _lockOn.Target;
        if (target == null) return;

        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        if (inputDirection.magnitude <= 0.1f) return;

        _currentSpeed = _strafeSpeed;

        Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.z;

        _characterController.Move(direction * _currentSpeed * Time.deltaTime);
    }

    private void QuickTurnAnim() => _animationHandler.PerformQuickTurn();

    public void DisableMove() => DisableMovement = true;
    public void EnableMove() => DisableMovement = false;

    private void Run(InputAction.CallbackContext context) => _run = true;

    private void Walk(InputAction.CallbackContext context) => _run = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,  transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,  DIRECTION);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(_groundCheck.position, _groundCheckRadius);
    }
#endif
}