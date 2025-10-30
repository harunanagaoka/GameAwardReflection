using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    [SerializeField, Tooltip("ノックバック方向")]
    private Vector3 m_blownAwayDirection;

    [SerializeField, Tooltip("ノックバック力")]
    private float m_blownAwayPower;

    [SerializeField, Tooltip("ノックバック時間")]
    private float m_blownAwayTime;

    [SerializeField, Tooltip("ダメージ")]
    private int m_damage;

    public Vector3 BlownAwayDirection => m_blownAwayDirection;

    public float BlownAwayPower => m_blownAwayPower;

    public float BlownAwayTime => m_blownAwayTime;

    public int Damage => m_damage;
}
