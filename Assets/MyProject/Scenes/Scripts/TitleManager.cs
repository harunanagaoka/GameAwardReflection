using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        
    }
    void Update()
    {
        var gamepad = Gamepad.current;

        if (Input.GetKeyDown((KeyCode.Space)))
        {
            SceneManager.LoadScene("Movie");
        }

        if(gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Movie");
        }

        if (Input.GetKeyDown((KeyCode.N)))
        {
            SceneManager.LoadScene("Movie");
        }
    }
}
