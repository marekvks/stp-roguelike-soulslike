using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFollowState : MonoBehaviour, AiState
{
    private AiStateId _id = AiStateId.FollowPlayer;
    private AiAgent _agent;
    private bool _activeState = false;

    [Header("Settings")]
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _followAngularSpeed = 360f;

    public void Enter(AiAgent agent)
    {
        _activeState = true;
        if (_agent == null)
            _agent = agent;

        _agent.NavMeshAgent.isStopped = false;
        _agent.Animator.SetBool("Run", true);
        _agent.SetNavMeshSpeed(_followSpeed, _followAngularSpeed);
        Debug.Log("follow");
    }

    public void Exit()
    {
        _activeState = false;
        _agent.NavMeshAgent.isStopped = true;
        _agent.Animator.SetBool("Run", false);
    }

    public AiStateId GetId()
    {
        return _id;
    }

    public void UpdateStateMachine()
    {
        if (Vector3.Distance(_agent.Player.position, _agent.gameObject.transform.position) <= 2)
        {
            _agent.SwitchState(AiStateId.Combat);
        }
        else if (Vector3.Distance(_agent.Player.position, _agent.gameObject.transform.position) >= 10)
        {
            _agent.SwitchState(AiStateId.Patrol);
        }

        _agent.NavMeshAgent.SetDestination(_agent.Player.position);
    }
}
