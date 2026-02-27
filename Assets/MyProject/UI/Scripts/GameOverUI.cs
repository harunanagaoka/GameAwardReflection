using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameOverUIRoot;
    [SerializeField]
    private GameObject m_gameClearUIRoot;

    private MainGameEvents m_mainGameEvents;

    private void Awake()
    {
        m_mainGameEvents = GetComponent<MainGameEvents>();
       // m_mainGameEvents.OnGameOver.AddListener(GameOver);
       // m_mainGameEvents.OnGameClear.AddListener(GameClear);
    }

    private void GameClear()
    {
        m_gameClearUIRoot.SetActive(true);
    }

    private void GameOver()
    {
        m_gameOverUIRoot.SetActive(true);
    }
}
