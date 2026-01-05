using UnityEngine;

[CreateAssetMenu(fileName = "TestAction", menuName = "Scriptable Objects/TestAction")]
public class TestActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new TestAction();
}

public class TestAction : StateAction
{
    public override void OnUpdate()
    {
        Debug.Log("StateA");
    }
}