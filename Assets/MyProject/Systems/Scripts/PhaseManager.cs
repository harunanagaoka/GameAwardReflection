using UnityEngine;

//フェーズはボスの体力ゲージごとに変化するのか時間ごと変化するのかが未定
//敵ごとにフェーズを持たせるのか、ゲーム全体でフェーズを管理するのかも未定
public enum GamePhase
{
    Phase1,
    Phase2,
    Phase3,
    PhaseLength
}
public class PhaseManager : MonoBehaviour
{
    [SerializeField]
    private GamePhase m_currentPhase = GamePhase.Phase1;

    public GamePhase CurrentPhase => m_currentPhase;

    public void SetPhase(GamePhase newPhase)
    {
        m_currentPhase = newPhase;
    }
}
