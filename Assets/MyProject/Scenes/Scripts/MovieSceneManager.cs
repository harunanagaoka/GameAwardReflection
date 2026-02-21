using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MovieSceneManager : MonoBehaviour
{
    [SerializeField]
    private string m_nextScene;
    void Update()
    {
        var gamepad = Gamepad.current;

        if (Input.GetKeyDown((KeyCode.Space)))
        {
            SceneManager.LoadScene(m_nextScene);
        }

        if (gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene(m_nextScene);
        }
    }
}
