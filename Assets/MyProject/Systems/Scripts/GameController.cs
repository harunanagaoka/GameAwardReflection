using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerGenerator m_playerGenerator;//仮置き　生成タイミングによりPhaseControllerに置いてもいいかも

    [SerializeField]
    private bool m_isDebug = false;

    [SerializeField]
    private float m_delayForNextPhase = 2;

    private PhaseController m_phaseController;
    private MainGameEvents m_mainGameEvents;
    private MainGameTimer m_timer;
    private VisualCuePlayerInMainScene m_visualCuePlayer;
    private PlayerDamageable m_player;

    private bool m_isGameStarted = false;
    private bool m_isGameOver = false;
    private bool m_isGameCleared = false;

    private bool m_isInited = false;
    private bool m_isInPresentation = false;

    private bool IsInGame => m_isGameStarted && !m_isGameOver && !m_isGameCleared;


    private void Awake()
    {
        m_phaseController = GetComponent<PhaseController>();
        m_mainGameEvents = GetComponent<MainGameEvents>();
        m_timer = GetComponent<MainGameTimer>();
        m_visualCuePlayer = GetComponent<VisualCuePlayerInMainScene>();
        m_visualCuePlayer.OnSceneEnterVisualCompleted += MainGameStart;
        m_visualCuePlayer.OnAllPhaseFinishedVisualCompleted += OnGameEndPresentationEnd;
        //m_visualCuePlayer.OnSceneExitVisualCompleted += GoNextScene;
        

        m_playerGenerator.GeneratePlayer();
        m_player = PlayerManager.Instance.Players[0].GetComponent<PlayerDamageable>();
    }

    private void Start()
    {
        m_phaseController.OnAllPhasesCompleted += OnAllPhasesCompleted;
        m_phaseController.OnPhaseEnd += OnPhaseTransition;
        m_phaseController.OnBossDestroyed += GoNextScene;
        m_visualCuePlayer.OnPhaseTransitionVisualCompleted += OnPhaseTransitionEnd;
    }

    private void OnDisable()
    {
        m_phaseController.OnAllPhasesCompleted -= OnAllPhasesCompleted;
        m_visualCuePlayer.OnSceneEnterVisualCompleted -= MainGameStart;
        m_phaseController.OnPhaseEnd -= OnPhaseTransition;
        m_phaseController.OnBossDestroyed -= GoNextScene;
        m_visualCuePlayer.OnPhaseTransitionVisualCompleted -= OnPhaseTransitionEnd;
        m_visualCuePlayer.OnAllPhaseFinishedVisualCompleted -= OnGameEndPresentationEnd;
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
            CheckGameEnd();
        }

        if (m_isGameOver)
        {
            if (!m_isInPresentation)
            {
                GoNextScene();
                m_isInPresentation = true;
            }
        }
    }

    private void MainGameStart()
    {
        m_timer.StartTimer();
        m_mainGameEvents.OnGameStart?.Invoke();
        m_isGameStarted = true;
    }

    private void CheckGameEnd()
    {
        if (m_player.HitPointRate <= 0)
        {
            TriggerGameOver();
        }

        //if (m_timer.CurrentTime <= 0)
        //{
        //    TriggerGameOver();
        //}
    }

    private void TriggerGameOver()
    {
        m_timer.StopTimer();
        m_isGameOver = true;
        Debug.Log("GameOver");
        m_mainGameEvents.OnGameOver?.Invoke();
    }

    private void OnAllPhasesCompleted()
    {
        if (m_isGameOver) return;

        m_isGameCleared = true;
        m_timer.StopTimer();
        PlayGameEndPresentation();
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

    private void PlayGameEndPresentation()
    {
        m_visualCuePlayer.PlayBossDefeat();
    }

    private void OnGameEndPresentationEnd()
    {
        m_mainGameEvents.OnGameFinishPresentationEnd?.Invoke();
        m_phaseController.OnAllGamePresentationEnd();
    }

    private void GoNextScene()
    {
        StartCoroutine(LoadSceneAfterDelay()); 
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(m_delayForNextPhase);

        if (m_isGameOver)
        {
            SceneManager.LoadScene("Defeat");
        }

        if (m_isGameCleared)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    private void OnPhaseTransition(int currentPhase, int nextPhase)
    {
        StartCoroutine(OnPhaseTransitionCoroutine(currentPhase, nextPhase));
        m_mainGameEvents.OnPhaseTransitionStart?.Invoke();
    }
    
    private IEnumerator OnPhaseTransitionCoroutine(int currentPhase,int nextPhase)
    {
        //フェーズおわりに呼ばれる
        m_visualCuePlayer.PlayTransitionEffect();
        yield return null;//デバッグ用
    }

    private void OnPhaseTransitionEnd()
    {
        m_mainGameEvents.OnPhaseTransitionEnd?.Invoke();//演出終わりに呼ばれる
    }
}
