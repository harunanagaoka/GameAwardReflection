using UnityEngine;

//アタックのフェーズSOを生成
//各フェーズのSOを所持させる

public class AttackPhaseFactory : MonoBehaviour
{
    [SerializeField]
    private AttackPhaseData[] m_attackPhaseDatas;

    public AttackPhaseData[] AttackPhaseDatas => m_attackPhaseDatas;

    public AttackPhaseData GetCurrentPhaseData(int phaseIndex)
    {
        int index = Mathf.Clamp(phaseIndex, 0 ,m_attackPhaseDatas.Length - 1);

        return m_attackPhaseDatas[index];
    }

    public AttackPhaseData GetAtkPhaseData(PhaseManager.EnemyPhase phase)//旧関数
    {
        return m_attackPhaseDatas[(int)phase];
    }



    //private void OnValidate()
    //{
    //    int length = (int)PhaseManager.EnemyPhase.PhaseLength;

    //    if (m_attackPhaseDatas == null || m_attackPhaseDatas.Length != length)
    //    {
    //        m_attackPhaseDatas = new AttackPhaseData[length];
    //    }
    //}
}

//現時点でフェーズ制になる予定のため、配列数はフェーズ数で固定する。
//フェーズを管理するクラスから、フェーズ数を取得し、配列数を決定する予定。
//Phase名はenum化する予定。


