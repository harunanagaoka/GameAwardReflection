using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private PlayerGenerator m_playerGenerator;

    [SerializeField]
    private EnemyManager m_enemyManager;

    [SerializeField]
    private EnemyData m_tutorialEnemyData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerGenerator.GeneratePlayer();
        m_enemyManager.Initialize();
        m_enemyManager.SpawnBossEnemy(m_tutorialEnemyData,0);
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.N)))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
