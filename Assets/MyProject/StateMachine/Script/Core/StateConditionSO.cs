using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateConditionSO", menuName = "Scriptable Objects/StateConditionSO")]
public abstract class StateConditionSO : ScriptableObject
{
    public StateCondition GetStateCondition(StateMachine stateMachine, bool expectedResult, Dictionary<ScriptableObject, object> createdInstances)
    {
        if (!createdInstances.TryGetValue(this, out var obj))
        {
            var condition = CreateCondition();
            condition.m_originSO = this;
            createdInstances.Add(this, condition);
            condition.Awake(stateMachine);

            obj = condition;
        }

        return new StateCondition(stateMachine, (Condition)obj, expectedResult);
    }
    protected abstract Condition CreateCondition();

}

public abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
{
    //必ず Condition を継承する、T は 引数なしコンストラクタ new T() ができることを指定している
    protected override Condition CreateCondition() => new T();
}
