using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : MonoBehaviour, AiState
{
    private AiStateId _id = AiStateId.Patrol;
    private int _currentDestinationIndex = 0;
    private AiAgent _agent;
    private bool _activeState = false;

    [Header("Settings")]
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2.5f;
    [SerializeField] private float _patrolAngularSpeed = 120f;

    public void Enter(AiAgent agent)
    {
        _activeState = true;
        if (_agent == null)
            _agent = agent;

        _agent.NavMeshAgent.isStopped = false;
        _agent.Animator.SetBool("Walk", true);
        _agent.SetNavMeshSpeed(_patrolSpeed, _patrolAngularSpeed);
        Debug.Log("Patrol");
    }

    public void Exit()
    {
        _activeState = false;
        _agent.Animator.SetBool("Walk", false);
        _agent.NavMeshAgent.isStopped = true;
    }

    public AiStateId GetId()
    {
        return _id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || !_activeState) return;

        if (_agent.Player == null) _agent.Player = other.transform;
        _agent.SwitchState(AiStateId.FollowPlayer);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.a = 0.2f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 2f);
    }

    public void UpdateStateMachine()
    {
        if (!_agent.NavMeshAgent.hasPath)
        {
            _agent.NavMeshAgent.SetDestination(_patrolPoints[_currentDestinationIndex].position);
            _currentDestinationIndex = _currentDestinationIndex == _patrolPoints.Length - 1 ? 0 : _currentDestinationIndex + 1;
        }
    }
}
