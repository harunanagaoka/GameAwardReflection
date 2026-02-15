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

        public PhaseChangeCondition ChangeCondition => changeCondition;

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

        public float ChangeThreshold => changeThreshold;

        public bool IsSatisfied(EnemyDamageable boss)
        {
            float hpRate = boss.HitPoint / boss.MaxHitPoint;
            return hpRate <= changeThreshold;
        }
    }

    //0~1でクランプ　1はこのフェーズの最大値　-1　0はこのフェーズの最小HP

    public float GetCurrentPhaseProgress(int currentPhaseIndex, EnemyDamageable boss)
    {
        float maxHPInCurrentPhase = 0f;
        float minHPInCurrentPhase = 0f;
        float currentHp = boss.HitPoint;

        if (currentPhaseIndex == 0) { 
            maxHPInCurrentPhase = boss.MaxHitPoint;
        }
        else 
        {
            maxHPInCurrentPhase = boss.MaxHitPoint * m_phaseDescriptors[currentPhaseIndex - 1].ChangeCondition.ChangeThreshold;
            maxHPInCurrentPhase -= 1f;
        }

        minHPInCurrentPhase = boss.MaxHitPoint * m_phaseDescriptors[currentPhaseIndex].ChangeCondition.ChangeThreshold;

        float normalizedHp = Mathf.Clamp01(
            (currentHp - minHPInCurrentPhase) / (maxHPInCurrentPhase - minHPInCurrentPhase));

        return normalizedHp;
    }
}
