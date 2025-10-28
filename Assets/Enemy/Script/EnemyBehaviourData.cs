using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewEnemyBehaviourData", menuName = "Scriptable Objects/NewEnemyBehaviourData")]
public class EnemyBehaviourData : ScriptableObject
{
    public enum EnemyBehaviourType
    {
        Straight,
        Following,
    }

    //public enum BulletDirectionName
    //{
    //    Straight,
    //    Following,
    //    Custom
    //}

   // [SerializeField, Header("íeÇÃêUÇÈïëÇ¢ÇÃÉäÉXÉg")]
    //private EnemyBehaviour[] m_EnemyBehaviours;

    //public EnemyBehaviour[] Behaviours { get { return m_EnemyBehaviours; } private set { m_EnemyBehaviours = value; } }

    [System.Serializable]
    public class BulletBehaviour
    {
        [SerializeField]
        private EnemyBehaviourType m_behaviour;

        [SerializeField]
        private Vector3 m_spawnPos;

        [SerializeField]
        private float m_speed;

        [SerializeField]
        private Vector3 m_customDirection;

        [SerializeField]
        private int m_repeatCount;

        [SerializeField]
        private float m_intervalTime;

        public EnemyBehaviourType Type { get { return m_behaviour; } private set { m_behaviour = value; } }

        public Vector3 SpawnPos { get { return m_spawnPos; } private set { m_spawnPos = value; } }

        public float Speed { get { return m_speed; }private set { m_speed = value; } }

        public Vector3 Direction { get { return m_customDirection; } private set { m_customDirection = value; } }

        public int RepeatCount { get { return m_repeatCount; } private set {m_repeatCount = value; } }

        public float IntervalTime {  get { return m_intervalTime; } private set { m_intervalTime = value; } }
    }
}
