using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackTimelineData", menuName = "Scriptable Objects/AttackTimelineData")]
public class AttackTimelineData : ScriptableObject
{

    [SerializeField]
    private AttackTimeline[] m_timelines;

    public AttackTimeline[] Timelines => m_timelines;

    /*
     Timeline
 „¥ Step0 (2s)
 „¥ Step1 (0.5s)
 „¥ Step2 (5s)
 „¤ ƒ‹[ƒv
     */


    [Serializable]
    public struct AttackTimeline
    {
        [SerializeField, Tooltip("“¯Žž‚É¶¬‚µ‚½‚¢UŒ‚‚ð“o˜^‚·‚é")]
        private AttackStepData[] m_stepDatas;

        [SerializeField]
        private float m_interval;

        public AttackStepData[] StepDatas => m_stepDatas;

        public float Interval => m_interval;
    }

    [Serializable]
    public struct AttackStepData
    {
        [SerializeField]
        private AttackData m_attackData;

        public AttackData AttackData => m_attackData;

    }
}
