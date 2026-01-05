
public class StateTransition : IStateComponent
{
    private State m_targetState;
    private StateCondition[] m_conditions;
    private int[] m_resultGroups;
    private bool[] m_results;
    public StateTransition() { }
    public StateTransition(State targetState, StateCondition[] conditions, int[] resultGroups = null)
    {
        m_targetState = targetState;
        m_conditions = conditions;
        m_resultGroups = resultGroups != null && resultGroups.Length > 0 ? resultGroups : new int[1];
        m_results = new bool[m_resultGroups.Length];
    }

    public bool TryGetTransiton(out State state)
    {
        state = ShouldTransition() ? m_targetState : null;
        return state != null;
    }

    public void OnStateEnter()
    {
        for (int i = 0; i < m_conditions.Length; i++)
        {
            m_conditions[i].condition.OnStateEnter();
        }

    }

    public void OnStateExit()
    {
        for (int i = 0; i < m_conditions.Length; i++)
        {
            m_conditions[i].condition.OnStateExit();
        }
    }

    private bool ShouldTransition()
    {

        int count = m_resultGroups.Length;
        for (int i = 0, idx = 0; i < count && idx < m_conditions.Length; i++)
            for (int j = 0; j < m_resultGroups[i]; j++, idx++)
                m_results[i] = j == 0 ?
                    m_conditions[idx].IsMet() :
                    m_results[i] && m_conditions[idx].IsMet();

        bool ret = false;
        for (int i = 0; i < count && !ret; i++)
            ret = ret || m_results[i];

        return ret;
    }
    internal void ClearConditionsCache()
    {
        for (int i = 0; i < m_conditions.Length; i++)
            m_conditions[i].condition.ClearStatementCache();
    }
}
