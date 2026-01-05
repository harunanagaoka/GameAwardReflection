using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/StateSO")]
public class StateSO : ScriptableObject
{
    [SerializeField] private StateActionSO[] m_actions = null;

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
        state.m_stateActions = GetActions(m_actions, stateMachine, createdInstances);

        return state;
    }

    private static StateAction[] GetActions(StateActionSO[] scriptableActions,
            StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
    {
        int count = scriptableActions.Length;
        var actions = new StateAction[count];
        for (int i = 0; i < count; i++)
        {
            actions[i] = scriptableActions[i].GetStateAction(stateMachine, createdInstances);
        }

        return actions;
    }
}
