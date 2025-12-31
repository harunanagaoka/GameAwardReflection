
public class StateTransition 
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
}
