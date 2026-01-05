public abstract class StateAction : IStateComponent
{
    public StateActionSO m_originSO;

    protected StateActionSO OriginSO => m_originSO;

    public abstract void OnUpdate();

    public virtual void Awake(StateMachine stateMachine) { }
    public void OnStateEnter(){ }

    public void OnStateExit(){ }

    public enum SpecificMoment
    {
        OnStateEnter, OnStateExit, OnUpdate,
    }
}
