using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TestActionB", menuName = "Scriptable Objects/TestActionB")]
public class TestActionSOB : StateActionSO
{
    protected override StateAction CreateAction() => new TestActionB();
}

public class TestActionB : StateAction
{
    public override void OnUpdate()
    {
        Debug.Log("StateB");
    }
}