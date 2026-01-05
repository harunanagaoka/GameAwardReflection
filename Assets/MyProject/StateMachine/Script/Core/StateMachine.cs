using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private TransitionTableSO m_transitionTableSO;

    private State m_currentState;

    private void Awake()
    {
        //最初のステートを生成し、ステートに入る
        m_currentState = m_transitionTableSO.InitConnectedStatesAndTransitions(this);
    }

    private void OnEnable()
    {
        UnityEditor.AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    }

    private void OnAfterAssemblyReload()
    {
        m_currentState = m_transitionTableSO.InitConnectedStatesAndTransitions(this);
    }

    private void OnDisable()
    {
        UnityEditor.AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
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
