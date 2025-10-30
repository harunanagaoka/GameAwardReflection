using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("“G‚ÌHP")]
    private int m_hitPoint;

    public int HP => m_hitPoint;
}