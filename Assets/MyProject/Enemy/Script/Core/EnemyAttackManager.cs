using UnityEngine;
using System.Collections;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField]
    private AttackPhaseData[] m_attackPhases;
    private PhaseController m_phaseContraller;//そのうちSOにする
    private AttackPhaseFactory m_phaseFactory;
    private AttackPhaseData m_currentPhaseAttacks;


    private void Awake()
    {
       m_phaseContraller = GetComponent<PhaseController>();
       m_phaseContraller.Initialize(m_attackPhases.Length);
       m_phaseFactory = GetComponent<AttackPhaseFactory>();
       InitCurrentPhase();
        //今のフェーズのアタックをもらう
    }

    private void InitCurrentPhase()
    {
        m_currentPhaseAttacks = m_attackPhases[m_phaseContraller.CurrentPhaseIndex];//あとでリファクタする
    }

    private IEnumerator ExcuteAttack()
    {
        yield return new WaitForSeconds(1);
    }

}
