using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool m_isDebug = false;

    [SerializeField]
    private PlayerGenerator m_playerGenerator;

    [SerializeField]
    private EnemyManager m_enemyManager;

    private PhaseController m_phaseController;

    private EnemyEvents m_bossEvents;

    private bool m_isInited = false;

    void Start()
    {
        m_phaseController = GetComponent<PhaseController>();
        m_enemyManager.Initialize();
    }

    void Update()
    {
        var gamepad = Gamepad.current;

        //デバッグ用：スペースキーまたはゲームパッドの〇ボタンでシーン遷移
        //if (Input.GetKeyDown((KeyCode.Space)))
        //{
        //    SceneManager.LoadScene("Main");
        //}

        //if (gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        //{
        //    SceneManager.LoadScene("Main");
        //}


        if (!m_isInited)
        {
            InitTutorial();
            m_isInited = true;
        }
    }

    private void InitTutorial()
    {
        m_playerGenerator.GeneratePlayer();
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
        // 敵が死んだら次のシーンへ
        SceneManager.LoadScene("Main");
    }
}

