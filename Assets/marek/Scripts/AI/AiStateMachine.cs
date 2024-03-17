using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    private AiState[] _aiStates;
    private AiAgent _agent;
    public AiState CurrentState;

    public AiStateMachine(AiAgent agent)
    {
        _agent = agent;
        int stateCount = System.Enum.GetNames(typeof(AiStateId)).Length;
        _aiStates = new AiState[stateCount];
    }

    public void SwitchState(AiStateId stateId)
    {
        CurrentState?.Exit();
        CurrentState = GetState(stateId);
        CurrentState?.Enter(_agent);
    }

    public AiState GetState(AiStateId stateId)
    {
        return _aiStates[(int)stateId];
    }

    public void RegisterState(AiState state)
    {
        _aiStates[(int)state.GetId()] = state;
    }

    public void Update()
    {
        CurrentState?.UpdateStateMachine();
    }
}
