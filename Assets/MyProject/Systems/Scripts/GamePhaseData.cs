using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePhaseData", menuName = "Scriptable Objects/GamePhaseData")]
public class GamePhaseData : ScriptableObject
{
    [SerializeField]
    private EnemyData m_bossData;

    [SerializeField]
    private PhaseDescriptor[] m_phaseDescriptors;

    public EnemyData BossData => m_bossData;

    public int PhaseCount => m_phaseDescriptors.Length;

    public PhaseDescriptor[] PhaseDescriptors => m_phaseDescriptors;

    [Serializable]
    public struct PhaseDescriptor
    {
        [SerializeField]
        private PhaseChangeCondition changeCondition;

        public bool CanChangePhase(EnemyDamageable boss)
        {
            return changeCondition.IsSatisfied(boss);
        }
    }

    [Serializable]
    public struct PhaseChangeCondition
    {
        [SerializeField]
        private float changeThreshold;

        public bool IsSatisfied(EnemyDamageable boss)
        {
            float hpRate = boss.HitPoint / boss.MaxHitPoint;
            return hpRate <= changeThreshold;
        }
    }
}
