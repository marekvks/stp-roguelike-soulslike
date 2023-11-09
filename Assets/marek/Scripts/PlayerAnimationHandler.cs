using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputHandler), typeof(PlayerMovement))]
public class PlayerAnimationHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetFloat("Speed", _playerMovement.CurrentSpeed);
        Vector3 movement = _inputHandler.GetMovement();
        if (movement.magnitude < 0.1f)
        {
            _animator.SetBool("Move", false);
            return;
        }
        _animator.SetBool("Move", true);
    }
}