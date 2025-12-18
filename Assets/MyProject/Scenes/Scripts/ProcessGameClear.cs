//責務がガバい、WaveManagerとの関係見直す
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // 新Input System用

public class ProcessGameClear : MonoBehaviour
{
    private MainGameEvents m_gameEvents;
    
    private bool m_isCleared = false;

    private bool m_isGameOvered = false;

    private void Start()
    {
        m_gameEvents = GetComponent<MainGameEvents>();
        m_gameEvents.OnGameOver.AddListener(() => m_isGameOvered = true);
        m_gameEvents.OnGameClear.AddListener(() => m_isCleared = true);
    }

    private void Update()
    {
        if (m_isGameOvered)
        {
            return;
        }

        if (m_isCleared)
        {
            Debug.Log("ゲームクリアー");
        }

        if (m_isCleared && Input.GetKeyDown(KeyCode.JoystickButton7) 
            || m_isCleared && Input.GetKeyDown(KeyCode.R))
        {
            
            SceneReload();
            return;
        }
    }


    private void CheckReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        m_isCleared = false;　//一度だけ処理を行うため
    }
}
