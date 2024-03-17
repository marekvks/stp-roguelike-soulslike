using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    Patrol,
    FollowPlayer,
    Combat,
    Death
}

public interface AiState
{
    public AiStateId GetId();
    public void Enter(AiAgent agent);
    public void Exit();
    public void UpdateStateMachine();
}
