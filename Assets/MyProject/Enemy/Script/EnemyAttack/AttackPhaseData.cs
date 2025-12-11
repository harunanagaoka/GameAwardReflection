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
    private AttackData[] m_attackDatas;

    [SerializeField]
    private PhaseChangeType m_phaseChangeType;

    [SerializeField]
    private float[] m_phaseChangeRates;

    [SerializeField]
    private float[] m_phaseChangeAmounts;

    public AttackData[] AttackDatas => m_attackDatas;

    public PhaseChangeType PhaseChangeType => m_phaseChangeType;

    public float[] PhaseChangeRates => m_phaseChangeRates;

    public float[] PhaseChangeAmounts => m_phaseChangeAmounts;

    //あるフェーズで使う攻撃のデータを持つ
}
