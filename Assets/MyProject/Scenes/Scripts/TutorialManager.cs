using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool m_isDebug = false;

    [SerializeField]
    private PlayerGenerator m_playerGenerator;

    [SerializeField]
    private EnemyManager m_enemyManager;

    private PhaseController m_phaseController;

    private VisualCuePlayerInTutorialScene m_visualCuePlayer;

    private EnemyEvents m_bossEvents;

    private bool m_isInited = false;


    void Awake()
    {
        m_phaseController = GetComponent<PhaseController>();
        m_visualCuePlayer = GetComponent<VisualCuePlayerInTutorialScene>();
        m_playerGenerator.GeneratePlayer();

        m_visualCuePlayer.OnSceneEnterVisualCompleted += InitTutorial;
        m_visualCuePlayer.OnSceneExitVisualCompleted += GoNextScene;
    }

    private void OnDisable()
    {
        m_visualCuePlayer.OnSceneEnterVisualCompleted -= InitTutorial;
        m_visualCuePlayer.OnSceneExitVisualCompleted -= GoNextScene;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (!m_isInited)
        {
            InitTutorial();
            m_isInited = true;
        }

        var gamepad = Gamepad.current;


    }

    private void InitTutorial()
    {
        
        m_phaseController.EnterTutorial();

        var boss = m_enemyManager.BossEnemy;
        if (boss != null)
        {
            m_bossEvents = boss.GetComponent<EnemyEvents>();
            if (m_bossEvents != null)
            {
                m_bossEvents.OnDeath.AddListener(OnBossDeath);
            }
        }
    }

    private void OnBossDeath()
    {
        // ìGÇ™éÄÇÒÇæÇÁéüÇÃÉVÅ[ÉìÇ÷
        m_visualCuePlayer.PlaySceneExitEffects();
    }

    private void GoNextScene()
    {
        SceneManager.LoadScene("Main");
    }
}

