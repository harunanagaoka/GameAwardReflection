using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMovePetternData", menuName = "Scriptable Objects/EnemyMovePetternData")]
public class EnemyMovePetternData : ScriptableObject
{
    [SerializeField, Tooltip("配列数をフェーズの数と必ず合わせてください。")]
    private MoveTimelineData[] m_moveTaskDatas;

    public MoveTimelineData[] MoveTaskDatas => m_moveTaskDatas;
}
