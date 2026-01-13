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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_isDebug)
        {
            m_playerGenerator.GeneratePlayer();
            m_enemyManager.Initialize();
            m_enemyManager.SpawnBossEnemy(m_tutorialEnemyData, 0);
        }
    }

    void Update()
    {
        var gamepad = Gamepad.current;

        if (Input.GetKeyDown((KeyCode.Space)))
        {
            SceneManager.LoadScene("Main");
        }

        if (gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
