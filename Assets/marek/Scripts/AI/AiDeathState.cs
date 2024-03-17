using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AiDeathState : MonoBehaviour, AiState
{
    private AiStateId _id = AiStateId.Death;
    private AiAgent _agent;
    private bool _activeState = false;

    public void Enter(AiAgent agent)
    {
        _activeState = true;
        if (_agent == null)
            _agent = agent;

        _agent.NavMeshAgent.isStopped = true;
        _agent.Enemy.Die();
        _agent.Animator.SetTrigger("Death");
        Debug.Log("Die");
    }

    public void Exit()
    {
        _activeState = false;
    }

    public AiStateId GetId()
    {
        return _id;
    }

    public void UpdateStateMachine()
    {
    }
}