using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackPatternData", menuName = "Scriptable Objects/EnemyAttackPatternData")]
public class EnemyAttackPatternData : ScriptableObject
{
    [SerializeField,Tooltip("配列数をフェーズの数と必ず合わせてください。")]
    private AttackTimelineData[] m_attackTaskDatas;

    public AttackTimelineData[] AttackTaskDatas => m_attackTaskDatas;
}
