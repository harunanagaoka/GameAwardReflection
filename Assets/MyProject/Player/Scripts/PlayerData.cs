using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Scriptable Objects/NewPlayerData")]
public class PlayerData : ScriptableObject
{
    //[SerializeField]
    //private Vector3 m_spawnPos;

    //[SerializeField]
    //private float m_speed;

    [SerializeField]
    private float m_maxDamageInterval = 0;

    //public Vector3 SpawnPos => m_spawnPos;

    //public float Speed => m_speed;

    public float DamageInterval => m_maxDamageInterval;

}
