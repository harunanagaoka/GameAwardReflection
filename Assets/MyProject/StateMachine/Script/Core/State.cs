
public class State
{
    public StateSO m_stateSO;
    public StateMachine m_stateMachine;
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
        void StateEnter(IStateComponent[] comps)
        {
            for (int i = 0; i < comps.Length; i++)
                comps[i].OnStateEnter();
        }
        StateEnter(m_transitions);
        StateEnter(m_stateActions);
    }

    public void OnUpdate()
    {
        for(int i = 0;i < m_stateActions.Length; i++)
        {
            m_stateActions[i].OnUpdate();
        }
    }

    public void OnStateExit()
    {
        void StateExit(IStateComponent[] comps)
        {
            for (int i = 0; i < comps.Length; i++)
                comps[i].OnStateExit();
        }
        StateExit(m_transitions);
        StateExit(m_stateActions);
    }

    /// <summary>
    /// //m_transitionに遷移判定を指示する
    /// </summary>
    /// <param name="state"></param>
    public bool CheckShouldTransition(out State state)
    {
        state = null;

        for(int i = 0; i < m_transitions.Length; i++)
        {
            if (m_transitions[i].TryGetTransiton(out state))
            {
                break;
            }
        }

        for(int i = 0;i < m_transitions.Length; i++)
        {
            m_transitions[i].ClearConditionsCache();
        }

        bool shouldTransition = state != null;
        return shouldTransition;
    }
}
