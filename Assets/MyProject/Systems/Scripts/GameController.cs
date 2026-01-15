using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerGenerator m_playerGenerator;//仮置き　生成タイミングによりPhaseControllerに置いてもいいかも

    [SerializeField]
    private bool m_isDebug = false;

    private PhaseController m_phaseController;
    private MainGameEvents m_mainGameEvents;
    private MainGameTimer m_timer;

    private bool m_isGameStarted = false;
    private bool m_isGameOver = false;
    private bool m_isGameCleared = false;

    private bool m_isInited = false;

    private bool IsInGame => m_isGameStarted && !m_isGameOver && !m_isGameCleared;


    private void Awake()
    {
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


        if (!m_isInited)
        {
            MainGameStart();
            m_isInited = true;
        }

        if (m_isGameOver || m_isGameCleared)
        {
            var gamepad = Gamepad.current;
            if (gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Title");
            }

            if (Input.GetKeyDown((KeyCode.Space)))
            {
                SceneManager.LoadScene("Title");
            }

        }

        if (!IsInGame)
        {
            return;
        }

        if (!m_isGameOver && !m_isGameCleared)
        {
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
        //m_isDebugをtrueにして再生後Sキーを押すとメインゲームスタートの処理を手動で呼べます
        if (!m_isGameStarted && !m_isGameOver && !m_isGameCleared && Input.GetKey(KeyCode.S))
        {
            MainGameStart();
        }

        //m_isDebugをtrueにした状態でゲームオーバー状態になった時、Rキーかゲームパッドでシーン再読み込みできます。
        if (m_isGameOver && Input.GetButtonDown("Fire4") || m_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentSceneName);
        }
    }
}
