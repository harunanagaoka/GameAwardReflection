using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

[CreateAssetMenu(fileName = "TransitionTableSO", menuName = "Scriptable Objects/TransitionTableSO")]
public class TransitionTableSO : ScriptableObject
{
    //遷移表を持つ。
    [SerializeField] private TransitionDescriptor[] m_transitions = default;
    //
    public State InitConnectedStatesAndTransitions(StateMachine stateMachine)
    {
        var states = new List<State>();
        var transitions = new List<StateTransition>();//stateに保存するためのtransitionのメモリ
        var createdInstances = new Dictionary<ScriptableObject, object>();//ダブり防止用

        var fromStates = m_transitions.GroupBy(transition => transition.FromState);

        foreach ( var fromState in fromStates)//つながりのあるステートを全て初期化する
        {
            if (fromState.Key == null) continue;

            //ステートの実体を生成
            State state = fromState.Key.CreateStateInstance(stateMachine,createdInstances);
            states.Add(state);

            transitions.Clear();
            foreach(var transitionDescriptor in fromState)
            {
                if (transitionDescriptor.ToState == null) continue;
                
                State toState = transitionDescriptor.ToState.CreateStateInstance(stateMachine,createdInstances);
                //ProcessConditionUsages(stateMachine, transitionItem.Conditions, createdInstances, out var conditions, out var resultGroups);
                transitions.Add(new StateTransition());//あとでコンストラクタ作成する
            }

            state.m_transitions = transitions.ToArray();
        }

        if (states.Count > 0)
        {
            return states[0];
        }
        else
        {
            Debug.Log("TransitionTableSOエラー");
            return null;
        }
    }

    //private void BuildStateConditionsAndSetupGroups(StateMachine stateMachine,
    //        ConditionUsage[] conditionUsages,
    //        Dictionary<ScriptableObject, object> createdInstances,
    //        out StateCondition[] conditions,
    //        out int[] resultGroups)
    //{
    //    int count = conditionUsages.Length;
    //    conditions = new StateCondition[count];
    //    for (int i = 0; i < count; i++)
    //        conditions[i] = conditionUsages[i].Condition.GetCondition(
    //            stateMachine, conditionUsages[i].ExpectedResult == Result.True, createdInstances);


    //    List<int> resultGroupsList = new List<int>();
    //    for (int i = 0; i < count; i++)
    //    {
    //        int idx = resultGroupsList.Count;
    //        resultGroupsList.Add(1);
    //        while (i < count - 1 && conditionUsages[i].Operator == Operator.And)
    //        {
    //            i++;
    //            resultGroupsList[idx]++;
    //        }
    //    }

    //    resultGroups = resultGroupsList.ToArray();
    //}

    [Serializable]
    public struct TransitionDescriptor
    {
        public StateSO FromState;
        public StateSO ToState;
        //public ConditionUsage[] Conditions;
    }
    public enum Result { True, False }
    public enum Operator { And, Or }
}
