//旧PhaseManager
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    [SerializeField]
    private int m_currentPhaseIndex = 0;

    private int m_phaseCount;

    public int CurrentPhaseIndex => m_currentPhaseIndex;

    public int PhaseCount => m_phaseCount;


    public void Initialize(int phaseCount)
    {
        m_phaseCount = phaseCount;
        m_currentPhaseIndex = 0;
    }

    public bool NextPhase()
    {
        if (m_currentPhaseIndex + 1 < m_phaseCount)
        {
            m_currentPhaseIndex++;
            return false;
        }
        return true; //最終フェーズのみtrueを返す
    }
}
