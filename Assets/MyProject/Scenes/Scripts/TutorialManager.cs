using UnityEditor.PackageManager;
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

    [SerializeField]
    private EnemyData m_tutorialEnemyData;

    private EnemyEvents m_bossEvents;

    void Start()
    {
        if (m_isDebug)
        {
            m_playerGenerator.GeneratePlayer();
            m_enemyManager.Initialize();
            m_enemyManager.SpawnBossEnemy(m_tutorialEnemyData, 0);

            // ボスのEnemyEvents取得＆OnDeath登録
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
    }

    private void OnBossDeath()
    {
        // 敵が死んだら次のシーンへ
        SceneManager.LoadScene("Main");
    }
}

