using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/StateSO")]
public class StateSO : ScriptableObject
{
    //[SerializeField] private StateActionSO[] _actions = null;

    public State CreateStateInstance(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
    {
        if (createdInstances.TryGetValue(this, out var created))
        {
            return (State)created;
        }
        
        var state = new State();
        createdInstances.Add(this, state);
        state.m_stateSO = this;
        state.m_stateMachine = stateMachine;
        state.m_transitions = new StateTransition[0];
        //state._actions = GetActions(_actions, stateMachine, createdInstances);ÇÃÇøÇ…çÏÇÈ

        return state;
    }
}
