using UnityEngine;

public enum PhaseChangeType
{
    HPRateBased,
    HPAmountBased
}

[CreateAssetMenu(fileName = "NewAttackPhaseData", menuName = "Scriptable Objects/AttackPhaseData")]
public class AttackPhaseData : ScriptableObject
{
    [SerializeField]
    private AttackSequence[] m_attackSequences;

    [SerializeField,Tooltip("旧データ　そのうち消します")]
    private AttackData[] m_attackDatas;

    [SerializeField, Tooltip("旧データ　そのうち消します")]
    private PhaseChangeType m_phaseChangeType;

    public AttackData[] AttackDatas => m_attackDatas;

    public PhaseChangeType PhaseChangeType => m_phaseChangeType;
}

[System.Serializable]
public class AttackSequence
{
    public AttackData Attack;
    public int Count;
    public float Interval;
    public Vector3[] SpawnOffsets;
}
