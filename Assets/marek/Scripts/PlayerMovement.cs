using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputHandler _inputHandler;

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _speedTransitionRate = 0.1f;
    private bool _run = false;
    private float _currentSpeed = 0f;

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
    }
    [SerializeField] private float _characterRotationSmoothness = 0.1f;

    private float yVelocity;

    private void Awake()
    {
        SubscribeToInput();
        // Set start speed
        _currentSpeed = _walkSpeed;
    }

    private void OnDestroy() => UnsubscribeFromInput();

    private void SubscribeToInput() => _inputHandler.SubscribeToRun(Run, Walk);

    private void UnsubscribeFromInput() => _inputHandler.UnsubscribeFromRun(Run, Walk);

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Handles character movement
    /// </summary>
    private void Move()
    {
        // Run logic
        if (_run)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _runSpeed, Time.deltaTime * _speedTransitionRate);
        else
            _currentSpeed = Mathf.Lerp(_currentSpeed, _walkSpeed, Time.deltaTime * _speedTransitionRate);

        Vector3 direction = _inputHandler.GetMovement().normalized;
        // Fixes rotation bugs
        if (direction.magnitude <= 0.1f) return;

        // Calculating the correct angle where should the character rotate according to the movement
        float yRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation, ref yVelocity, _characterRotationSmoothness);
        transform.rotation = Quaternion.Euler(0f, smoothRotation, 0f);

        _characterController.Move(direction * _currentSpeed * Time.deltaTime);
    }

    private void Run(InputAction.CallbackContext context) => _run = true;

    private void Walk(InputAction.CallbackContext context) => _run = false;
}