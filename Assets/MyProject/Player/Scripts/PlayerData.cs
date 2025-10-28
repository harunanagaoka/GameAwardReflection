using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Scriptable Objects/NewPlayerData")]
public class PlayerBehaviourData : ScriptableObject
{
    public enum PlayerBehaviourType
    {
        Straight,
        Following,
    }


    [SerializeField, Header("プレイヤーの振舞いのリスト")]
    private PlayerBehaviour[] m_PlayerBehaviours;

    public PlayerBehaviour[] Behaviours { get { return m_PlayerBehaviours; } private set { m_PlayerBehaviours = value; } }

    [System.Serializable]
    public class PlayerBehaviour
    {
        [SerializeField]
        private float m_MaxHP;

        [SerializeField]
        private Vector3 m_spawnPos;

        [SerializeField]
        private float m_speed;

        [SerializeField]
        private Vector3 m_customDirection;


        public float MaxHP { get { return m_MaxHP; } private set { m_MaxHP = value; } }

        public Vector3 SpawnPos { get { return m_spawnPos; } private set { m_spawnPos = value; } }

        public float Speed { get { return m_speed; } private set { m_speed = value; } }

        public Vector3 Direction { get { return m_customDirection; } private set { m_customDirection = value; } }


    }
}
