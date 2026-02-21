using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    FadeEffect m_fadeEffect;

    bool m_goNextScene = false;
    private void Start()
    {
        StartCoroutine(m_fadeEffect.Fade(false));
    }
    void Update()
    {
        if (m_goNextScene)
        {
            return;
        }

        var gamepad = Gamepad.current;

        if (Input.GetKeyDown((KeyCode.Space)))
        {
            m_goNextScene = true;
            StartCoroutine(GoNextScene());
        }

        if(gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            m_goNextScene = true;
            StartCoroutine(GoNextScene());
        }
    }

    private IEnumerator GoNextScene()
    {
       
        yield return StartCoroutine(m_fadeEffect.Fade(true));
        SceneManager.LoadScene("Movie");
    }
}
