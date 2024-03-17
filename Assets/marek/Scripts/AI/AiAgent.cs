using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private AiStateId _initialState;
    private AiStateMachine _stateMachine;
    public NavMeshAgent NavMeshAgent;
    public Animator Animator;
    public Enemy Enemy;
    [HideInInspector] public Transform Player;

    [Header("States")]
    [SerializeField] private AiPatrolState _patrolState;
    [SerializeField] private AiFollowState _followState;
    [SerializeField] private AiCombatState _combatstate;
    [SerializeField] private AiDeathState _deathState;

    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        _stateMachine = new AiStateMachine(this);
        RegisterStates();
        SwitchState(_initialState);
    }

    private void FixedUpdate()
    {
        if (Enemy.Health <= 0 && !Enemy.Dead)
            SwitchState(AiStateId.Death);

        _stateMachine.Update();
    }

    public void SwitchState(AiStateId id) => _stateMachine.SwitchState(id);

    private void RegisterStates()
    {
        _stateMachine.RegisterState(_patrolState);
        _stateMachine.RegisterState(_followState);
        _stateMachine.RegisterState(_combatstate);
        _stateMachine.RegisterState(_deathState);
    }

    public void SetNavMeshSpeed(float speed, float angularSpeed)
    {
        NavMeshAgent.speed = speed;
        NavMeshAgent.angularSpeed = angularSpeed;
    }
}
