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
        private PhaseData m_phaseData;

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
        [SerializeField,Tooltip("絶対に0～1の範囲で設定すること")]//絶対に0～1の範囲にすること！！制限しようとしたけどバグでできませんでした・・・
        private float changeThreshold;

        public bool IsSatisfied(EnemyDamageable boss)
        {
            float hpRate = boss.HitPoint / boss.MaxHitPoint;
            return hpRate <= changeThreshold;
        }
    }
}
