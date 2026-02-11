using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMoveTimelineData", menuName = "Scriptable Objects/EnemyMoveTimelineData")]
public class MoveTimelineData : ScriptableObject
{

    [SerializeField]
    private MoveTimeline[] m_timelines;

    public MoveTimeline[] Timelines => m_timelines;

    /*
     Timeline
 „¥ Step0 (2s)
 „¥ Step1 (0.5s)
 „¥ Step2 (5s)
 „¤ ƒ‹[ƒv
     */


    [Serializable]
    public struct MoveTimeline
    {
        [SerializeField, Tooltip("“¯Žž‚É¶¬‚µ‚½‚¢“®‚«‚ð“o˜^‚·‚é")]
        private MoveStepData[] m_stepDatas;

        [SerializeField]
        private float m_interval;

        public MoveStepData[] StepDatas => m_stepDatas;

        public float Interval => m_interval;
    }

    [Serializable]
    public struct MoveStepData
    {
        [SerializeField]
        private Vector3 m_targetPos;

        [SerializeField]
        private float m_moveDuration;

        [SerializeField]
        private float m_velocity;

        public Vector3 TargetPos => m_targetPos;

        public float MoveDuration => m_moveDuration;

        public float Velocity => m_velocity;

    }
}
