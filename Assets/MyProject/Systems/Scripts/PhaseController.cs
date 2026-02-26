using System;
using UnityEngine;

//現在のフェーズの情報を持つ、ChangePhaseの号令を出す
public class PhaseController : MonoBehaviour
{
    [SerializeField]
    private GamePhaseData m_gamePhaseData;

    [SerializeField]
    private EnemyManager m_enemyManager;

    private MainGameEvents m_mainGameEvents;

    private EnemyDamageable m_bossEnemy;

    private int m_currentPhase = 0;

    private bool m_isGameEnded = false;

    private bool m_isGameStarted = false;

    public event Action<int> OnPhaseStarted;

    public event Action<int,int> OnPhaseEnd;

    public event Action<int,int> OnPhaseChanged;

    public event Action OnAllPhasesCompleted;

    public event Action OnBossDestroyed;

    private bool m_isPhaseTransition = false;

    private bool IsInPhase => !m_isGameEnded && m_isGameStarted;

    private void Start()
    {
        m_mainGameEvents = GetComponent<MainGameEvents>();
        m_mainGameEvents.OnGameStart.AddListener(EnterFirstPhase);
        m_mainGameEvents.OnGameOver.AddListener(()=> m_isGameEnded = true);
        m_mainGameEvents.OnPhaseTransitionEnd.AddListener(NextPhase);
        m_enemyManager.OnBossEnemyDied += (HandleBossEnemyDied);
        m_enemyManager.Initialize(m_mainGameEvents);

        m_currentPhase = 0;
        if (!m_bossEnemy)
        {
            m_bossEnemy = m_enemyManager.SpawnBossEnemy(m_gamePhaseData.BossData, m_currentPhase);
        }
        
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossEnemyDied -= (HandleBossEnemyDied);
    }

    private void Update()
    {
        if (!m_isPhaseTransition && IsInPhase && m_gamePhaseData.PhaseDescriptors[m_currentPhase].CanChangePhase(m_bossEnemy))
        {
            m_isPhaseTransition = true;
            OnPhaseEnd?.Invoke(m_currentPhase, m_currentPhase + 1);

            //NextPhase();//デリゲートで呼ぶようにしました
        }
    }

    private void EnterFirstPhase()
    {
        m_isGameStarted = true;
        m_enemyManager.SetEnemyPhase(m_currentPhase);
        OnPhaseStarted?.Invoke(m_currentPhase);
    }

    public void EnterTutorial()
    {
        if (!m_bossEnemy)
        {
            m_enemyManager.Initialize(m_mainGameEvents);
            m_bossEnemy = m_enemyManager.SpawnBossEnemy(m_gamePhaseData.BossData, m_currentPhase);
        }
        m_isGameStarted = true;
        m_enemyManager.SetEnemyPhase(m_currentPhase);
        OnPhaseStarted?.Invoke(m_currentPhase);
    }

    public void NextPhase()
    {
        int prevPhase = m_currentPhase;
        int nextPhase = m_currentPhase + 1;

        if (nextPhase >= m_gamePhaseData.PhaseCount)
        {
            //OnAllPhasesCompleted?.Invoke();
            return;
        }

        m_currentPhase = nextPhase;

        m_enemyManager.SetEnemyPhase(m_currentPhase);

        m_isPhaseTransition = false;

        OnPhaseChanged?.Invoke(prevPhase, nextPhase);
    }

    private void HandleBossEnemyDied()
    {
        OnAllPhasesCompleted?.Invoke();
    }

    public float GetCurrentPhaseProgress()
    {
        return m_gamePhaseData.GetCurrentPhaseProgress(m_currentPhase, m_bossEnemy);
    }

    public void OnAllGamePresentationEnd()
    {
        m_enemyManager.EnemyDestroy();
        OnBossDestroyed?.Invoke();
    }
}
