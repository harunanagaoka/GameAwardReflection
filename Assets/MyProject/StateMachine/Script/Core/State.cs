
public class State
{
    public StateSO m_stateSO;
    public StateMachine m_stateMachine;//ひとまずはPlayerのみ
    public StateTransition[] m_transitions;
    public StateAction[] m_stateActions;

    public State() { }

    //引数アリコンストラクタ
    public State(StateSO stateSO,StateMachine stateMachine,StateTransition[] transitions, StateAction[] actions)
    {
        m_stateSO = stateSO;
        m_stateMachine = stateMachine;
        m_transitions = transitions;
        m_stateActions = actions;
    }

    //Stateのあらゆる情報を持つ。
    //transition,Action,StateMachine,StateSO
    //また、上記を構造体として保持する
    public void OnStateEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnStateExit()
    {

    }
    
    public bool CheckShouldTransition(out State state)
    {
        state = null;

        //m_transitionに遷移判定を指示する


        bool shouldTransition = state != null;
        return shouldTransition;
    }
}
