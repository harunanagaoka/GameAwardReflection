using UnityEngine;
using static PhaseManager;

public class PhaseWatcher : MonoBehaviour
{//AttackPhaseの変更基準を監視して、フェーズ変更の通知を行う

    private AttackPhaseData[] m_attackPhaseDatas;

    private AttackPhaseData m_currentPhaseData;

    private EnemyDamageable m_enemyDamageable;

    private PhaseChangeType m_phaseChangeType;

    private PhaseManager m_phaseManager;

    private float[] m_changePhaseAmount;//フェーズ数と絶対同じだという保証がほしいが、今はなし

    void Start()
    {
        m_enemyDamageable = TryGetComponent<EnemyDamageable>(out var damageable) ? damageable : null;
        m_phaseManager = GetComponent<PhaseManager>();
    }

    void Update()
    {
        if(m_enemyDamageable == null || m_changePhaseAmount == null)
        {
            return;
        }

        float currentHP = m_enemyDamageable.HitPoint;

        switch (m_phaseChangeType)
        {
            case PhaseChangeType.HPRateBased:
                //一気にフェーズが飛ぶ可能性があるのでループで再帰的に確認する必要がある

                float hpRate = currentHP / m_enemyDamageable.MaxHitPoint;
                if (hpRate <= m_changePhaseAmount[(int)m_phaseManager.CurrentPhase])
                {
                    ChangePhase();
                }

                break;
            case PhaseChangeType.HPAmountBased:

                if (currentHP <= m_changePhaseAmount[(int)m_phaseManager.CurrentPhase])
                {
                    ChangePhase();
                }

                break;
            default:
                break;
        }
    }

    public void SetCurrentPhaseData(AttackPhaseData[] phaseData, EnemyPhase phase)
    {
        m_attackPhaseDatas = (AttackPhaseData[])phaseData.Clone();
        m_currentPhaseData = phaseData[(int)phase];
        InitPhase();
    }

    public void InitPhase()
    {
        m_phaseChangeType = m_currentPhaseData.PhaseChangeType;

        switch (m_phaseChangeType)
        {
            case PhaseChangeType.HPRateBased:

                float[] copyRate = (float[])m_currentPhaseData.PhaseChangeRates.Clone();
                m_changePhaseAmount = copyRate;

                break;
            case PhaseChangeType.HPAmountBased:

                float[] copyAm = (float[])m_currentPhaseData.PhaseChangeAmounts.Clone();
                m_changePhaseAmount = copyAm;

                break;
            default:
                break;
        }
    }

    private void ChangePhase()
    {
        //m_phaseManager.NextPhase();
        //m_currentPhaseData = m_attackPhaseDatas[(int)m_phaseManager.CurrentPhase];
    }


}
