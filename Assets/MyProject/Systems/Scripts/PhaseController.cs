using UnityEngine;

//現在のフェーズの情報を持つ、ChangePhaseの号令を出す
public class PhaseController : MonoBehaviour
{
    [SerializeField]
    private GamePhaseData m_gamePhaseData;

    private EnemyDamageable m_bossEnemy;

    EnemySpawner m_enemySpawner;

    private int m_currentPhase = 0;

    private void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
        EnterFirstPhase();
    }

    private void Update()
    {
        if (m_gamePhaseData.PhaseDescriptors[m_currentPhase].CanChangePhase(m_bossEnemy))
        {
            NextPhase();
        }
    }

    private void EnterFirstPhase()
    {
        m_currentPhase = 0;
        m_bossEnemy = m_enemySpawner.SpawnEnemy(m_gamePhaseData.BossData);
    }

    public bool NextPhase()
    {
        int nextPhase = m_currentPhase + 1;

        if (nextPhase >= m_gamePhaseData.PhaseCount)
        {
            // これ以上進めない（最終フェーズ）
            return true;
        }

        m_currentPhase = nextPhase;
        //OnPhaseChanged(m_currentPhase);

        return m_currentPhase == m_gamePhaseData.PhaseCount - 1;
    }
}
