using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackPhaseData", menuName = "Scriptable Objects/AttackPhaseData")]

public class AttackPhaseData : ScriptableObject
{
    [SerializeField]
    private AttackData[] m_attackDatas;

    public AttackData[] AttackDatas => m_attackDatas;

    //あるフェーズで使う攻撃のデータを持つ
}
