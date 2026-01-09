using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool m_isDebug = false;

    private PhaseController m_phaseController;
    private PlayerGenerator m_playerGenerator;//仮置き　生成タイミングによりPhaseControllerに置いてもいいかも
    private MainGameEvents m_mainGameEvents;
    private MainGameTimer m_timer;

    private bool m_isGameStarted = false;
    private bool m_isGameOver = false;
    private bool m_isGameCleared = false;

    private bool IsInGame => m_isGameStarted && !m_isGameOver && !m_isGameCleared;


    private void Awake()
    {
        m_playerGenerator = GetComponent<PlayerGenerator>();
        m_phaseController = GetComponent<PhaseController>();
        m_mainGameEvents = GetComponent<MainGameEvents>();
        m_timer = GetComponent<MainGameTimer>();
    }

    private void Start()
    {
        m_phaseController.OnAllPhasesCompleted += OnAllPhasesCompleted;
    }

    private void OnDisable()
    {
        m_phaseController.OnAllPhasesCompleted -= OnAllPhasesCompleted;
    }


    void Update()
    {
        if (m_isDebug)
        {
            HandleDebugInput();
        }

        if (!IsInGame)
        {
            return;
        }

        if (!m_isGameOver && !m_isGameCleared)
        {
            //クリア判定とゲームオーバー判定
            CheckGameEnd();
        }
    }

    private void MainGameStart()
    {
        m_timer.StartTimer();
        m_playerGenerator.GeneratePlayer();
        m_mainGameEvents.OnGameStart?.Invoke();
        m_isGameStarted = true;
    }

    private void CheckGameEnd()
    {
        if (m_timer.CurrentTime <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        m_mainGameEvents.OnGameOver?.Invoke();
        m_timer.StopTimer();
        m_isGameOver = true;
        Debug.Log("GameOver");
    }

    private void OnAllPhasesCompleted()
    {
        if (m_isGameOver) return;

        m_isGameCleared = true;
        m_mainGameEvents.OnGameClear?.Invoke();

        Debug.Log("GameClear");
    }

    private void HandleDebugInput()
    {
        if (!m_isGameStarted && !m_isGameOver && !m_isGameCleared && Input.GetKey(KeyCode.S))
        {
            MainGameStart();
        }

        if (m_isGameOver && Input.GetButtonDown("Fire4") || m_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // 現在のシーン名を取得
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 同じシーンをロード（再読み込み）
            SceneManager.LoadScene(currentSceneName);
        }
    }

    //ゲームオーバー　敵がゼロでないのに制限時間が切れる
    //ゲームクリア―　制限時間内に敵をゼロにする
    //クリアーの方は制限時間がゼロでなければ　でいいのか？
}
