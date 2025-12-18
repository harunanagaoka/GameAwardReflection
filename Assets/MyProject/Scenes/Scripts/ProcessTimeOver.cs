using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessTimeOver : MonoBehaviour
{
    private MainGameEvents m_mainGameEvents;
    private MainGameTimer m_mainGameTimer;
    private bool m_isGameOver = false;

    void Start()
    {
        m_mainGameTimer = GetComponent<MainGameTimer>();
        m_mainGameEvents = GetComponent<MainGameEvents>();
    }

    void Update()
    { 
        if (!m_isGameOver && m_mainGameTimer.CurrentTime <= 0)//CurrentTimeが0以下になったら
        {
            m_mainGameEvents.OnGameOver?.Invoke();

            Debug.Log("GameOver");

            m_isGameOver = true;
        }

        if (m_isGameOver && Input.GetButtonDown("Fire4") || m_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // 現在のシーン名を取得
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 同じシーンをロード（再読み込み）
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
