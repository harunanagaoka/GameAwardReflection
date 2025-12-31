using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private TransitionTableSO m_transitionTable;

    private State m_currentState;
    //StateとStateクラスをもつ
    private void Awake()
    {
        //最初のステートを生成し、ステートに入る
        m_currentState = m_transitionTable.InitConnectedStatesAndTransitions(this);
    }

    private void Start()
    {
        m_currentState.OnStateEnter();
    }

    private void Update()
    {
        //遷移チェック
        if(m_currentState.CheckShouldTransition(out var nextState))
        {
            m_currentState.OnStateExit();
            m_currentState = nextState;
            m_currentState.OnStateEnter();
        }

        //現在のStateのUpdate
        m_currentState.OnUpdate();
    }
}
