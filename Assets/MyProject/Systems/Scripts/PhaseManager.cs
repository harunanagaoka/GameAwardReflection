using UnityEngine;

//フェーズはボスの体力ゲージごとに変化するのか時間ごと変化するのかが未定
//フェーズは敵ごとに管理するべきだが・・・。
public class PhaseManager : MonoBehaviour
{
    public enum EnemyPhase
    {
        Phase1,
        Phase2,
        Phase3,
        PhaseLength
    }

    [SerializeField]
    private EnemyPhase m_currentPhase = EnemyPhase.Phase1;

    public EnemyPhase CurrentPhase => m_currentPhase;

    private void Start()
    {
        m_currentPhase = EnemyPhase.Phase1;
    }

    public void SetPhase(EnemyPhase newPhase)
    {
        m_currentPhase = newPhase;
    }

    public bool NextPhase()
    {
        bool isFinalPhase = false;

        if (m_currentPhase < EnemyPhase.PhaseLength - 1)
        {
            m_currentPhase++;

        }

        if (m_currentPhase == EnemyPhase.PhaseLength - 1)
        {
            isFinalPhase = true;
        }
       

        return isFinalPhase;
    }
}
