using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    //途中でノックバック方向の仕様が変更されたため、コメントアウトしています。
    //[SerializeField, Tooltip("ノックバック方向")]
    //private Vector3 m_blownAwayDirection;

    [SerializeField]
    private Vector3 m_initPos = Vector3.zero;//攻撃の初期位置

    [SerializeField, Tooltip("攻撃のプレハブ")]
    private GameObject m_attackPrefab;

    [SerializeField, Tooltip("攻撃予告のプレハブ")]
    private GameObject m_attackRange;

    [SerializeField,Tooltip("攻撃予告の時間")]
    private float m_warningDuration = 1f;

    [SerializeField, Tooltip("ノックバック力")]
    private float m_blownAwayPower;

    [SerializeField, Tooltip("ノックバック時間")]
    private float m_blownAwayTime;

    [SerializeField, Tooltip("ダメージ")]
    private float m_timePenalty;

    [SerializeField, Tooltip("攻撃の持続時間")]
    private float m_lifeTime = 1.0f;

    public GameObject AttackPrefab => m_attackPrefab;

    public GameObject AttackRange => m_attackRange;

    public Vector3 InitPos => m_initPos;

    public float WarningDuration => m_warningDuration;

    public float BlownAwayPower => m_blownAwayPower;

    public float BlownAwayTime => m_blownAwayTime;

    public float TimePenalty => m_timePenalty;

    public float LifeTime => m_lifeTime;

}

