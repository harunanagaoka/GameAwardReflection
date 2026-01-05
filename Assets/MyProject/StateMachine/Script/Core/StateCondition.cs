public abstract class Condition : IStateComponent
{
    private bool m_isCached = false;
    private bool m_cashedStatement = default;//なんでここだけfalseじゃなくてdefault？
    public StateConditionSO m_originSO;

    protected StateConditionSO OriginSO => m_originSO;

    /// <summary>
    /// 遷移条件そのものです。継承先で具体的な判定を設定します。
    /// </summary>

    protected abstract bool Statement();

    public bool GetStatement()
    {
        if (!m_isCached)
        {
            m_isCached = true;
            m_cashedStatement = Statement();
        }

        return m_cashedStatement;
    }
    public void ClearStatementCache()
    {
        m_isCached = false;
    }

    public virtual void Awake(StateMachine stateMachine) { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}

/// <summary>
/// 遷移条件が満たされているか確認するための構造体
/// </summary>
public readonly struct StateCondition
{
    public readonly StateMachine stateMachine;
    public readonly Condition condition;
    public readonly bool expectedResult;//このCondition(条件)がtrue,falseどっちになってほしいかを設定する値

    public StateCondition(StateMachine stateMachine, Condition condition, bool expectedResult)
    {
        this.stateMachine = stateMachine;
        this.condition = condition;
        this.expectedResult = expectedResult;
    }

    //遷移条件を満たしているかどうかを返します。
    //IsMet　→　meet a condition　→　○○を満たしている、適合しているみたいな言葉です
    public bool IsMet()
    {
        bool statement = this.condition.GetStatement();//Conditionに設定した条件がtrueかfalseか
        bool isMet = statement == this.expectedResult;//現在の状態を見た結果、それが期待したものであったかの評価

        return isMet;
    }
}
