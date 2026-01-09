using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackTimelineData", menuName = "Scriptable Objects/AttackTimelineData")]
public class AttackTimelineData : ScriptableObject
{
    [SerializeField, Tooltip("ひとつのフェーズ内での攻撃パターンを設定できます。")]
    private AttackStepData[] m_stepDatas;

    [SerializeField]
    private AttackTimelineData[] m_timelines;

    public AttackStepData[] StepDatas => m_stepDatas;

    public AttackTimelineData[] Timelines => m_timelines;

    /*
     Timeline
 ├ Step0 (2s)
 ├ Step1 (0.5s)
 ├ Step2 (5s)
 └ ループ
     */


    [Serializable]
    public struct AttackTimeline
    {
        [SerializeField, Tooltip("同時に生成したい攻撃を登録する")]
        private AttackStepData[] m_stepDatas;

        public AttackStepData[] StepDatas => m_stepDatas;
    }

    [Serializable]
    public struct AttackStepData
    {
        [SerializeField]
        private AttackData m_attackData;

        [SerializeField]
        private float m_interval;

        public AttackData AttackData => m_attackData;

        public float Interval => m_interval;
    }
}
