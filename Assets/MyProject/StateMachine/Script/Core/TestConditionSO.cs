using UnityEngine;

[CreateAssetMenu(fileName = "TestCondition", menuName = "Scriptable Objects/TestCondition")]
public class TestConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new TestCondition();
}

public class TestCondition : Condition
{
    private int num = 5;
    protected override bool Statement()
    {
        bool isUnderTen = num < 10;
        return isUnderTen;
    }
}