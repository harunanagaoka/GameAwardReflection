using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("敵のHP")]
    private int m_hitPoint;

    [SerializeField, Tooltip("ダメージを受けた時の色")]
    private Color m_onDamageColor = Color.red;

    [SerializeField, Tooltip("ダメージ演出の時間")]
    private float m_damageEffectTime = 0.5f;

    public int HP => m_hitPoint;

    public Color OnDamageColor => m_onDamageColor;

    public float DamageEffectTime => m_damageEffectTime;
}