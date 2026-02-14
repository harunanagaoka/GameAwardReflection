using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("敵のプレハブ")]
    private GameObject m_prefab;

    [SerializeField,Tooltip("敵の初期位置")]
    private Vector3 m_initPosition;

    [SerializeField, Tooltip("敵のHP")]
    private int m_hitPoint;

    [SerializeField, Tooltip("ダメージを受けた時の色")]
    private Color m_onDamageColor = Color.red;

    [SerializeField, Tooltip("ダメージを受けるインターバル時間")]
    private float m_damageInterval = 0.5f;

    [SerializeField, Tooltip("ダメージ演出の時間")]
    private float m_damageEffectTime = 0.5f;

    [SerializeField,Tooltip("敵の攻撃パターンが登録できます")]
    EnemyAttackPatternData m_enemyAttackPatternData;

    [SerializeField,Tooltip("フェーズが変わるHP割合・Rate以下になるごとにひとつ進みます")]
    private float[] m_phaseChangeRates;

    [SerializeField]
    private float[] m_phaseChangeAmounts;

    public GameObject Prefab => m_prefab;

    public Vector3 InitPos => m_initPosition;

    public int HP => m_hitPoint;

    public Color OnDamageColor => m_onDamageColor;

    public float DamageInterval => m_damageInterval;

    public float DamageEffectTime => m_damageEffectTime;

    public EnemyAttackPatternData AttackPetternData => m_enemyAttackPatternData;

    public float[] PhaseChangeRates => m_phaseChangeRates;

    public float[] PhaseChangeAmounts => m_phaseChangeAmounts;
}