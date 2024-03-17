using UnityEngine;

public class AiCombatState : MonoBehaviour, AiState
{
    private AiStateId _id = AiStateId.Combat;
    private AiAgent _agent;
    private float _nextAttackTime = 0f;
    private bool _activeState = false;

    [Header("Settings")]
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private float _attackRate = 10f;

    // Prep
    private void Start()
    {
        DisableWeaponCollider();
    }

    public void Enter(AiAgent agent)
    {
        _activeState = true;
        if (_agent == null)
            _agent = agent;

        Debug.Log("Combat");
    }

    public void Exit()
    {
        _activeState = false;
    }

    public AiStateId GetId()
    {
        return _id;
    }

    private void Attack()
    {
        _nextAttackTime = Time.time + _attackRate;

        // 10% chance
        bool heavyAttack = Random.Range(1, 11) == 1;

        if (heavyAttack)
            _agent.Animator.SetTrigger("Heavy Attack");
        else
            _agent.Animator.SetTrigger("Attack");
    }

    private void SetRotation()
    {
        Transform transform = _agent.gameObject.transform;
        transform.LookAt(_agent.Player);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }

    public void TryCombo()
    {
        bool combo = Random.Range(1, 3) == 1;
        if (!combo) return;

        _agent.Animator.SetTrigger("Combo");
    }

    public void EnableWeaponCollider() => _weaponCollider.enabled = true;
    public void DisableWeaponCollider() => _weaponCollider.enabled = false;

    public void UpdateStateMachine()
    {
        if (Vector3.Distance(_agent.Player.position, _agent.gameObject.transform.position) >= 3)
        {
            _agent.SwitchState(AiStateId.FollowPlayer);
        }

        SetRotation();

        if (Time.time >= _nextAttackTime)
        {
            Attack();
        }
    }
}