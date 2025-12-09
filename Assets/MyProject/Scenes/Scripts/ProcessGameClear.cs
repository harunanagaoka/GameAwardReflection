using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // 新Input System用

public class ProcessGameClear : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager; 
    private bool isCleared = false;

    private void Update()
    {
        if (isCleared)
        {
            CheckReloadInput();
            return;
        }

        if (waveManager != null && waveManager.IsCleared)
        {
            isCleared = true;
            Debug.Log("ゲームクリアー");
        }
    }


    private void CheckReloadInput()
    {
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }
}
