using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class MovieSceneManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer m_videoPlayer;

    [SerializeField]
    private string m_nextScene;

    [SerializeField]
    private FadeEffect m_fadeEffect;

    private bool m_isFinishedMovie = false;

    private void Start()
    {
        m_videoPlayer.loopPointReached += OnVideoEnd;
        StartCoroutine(EnterScene());

    }

    private void OnDisable()
    {
        m_videoPlayer.loopPointReached -= OnVideoEnd;
    }



    void Update()
    {
        if(m_isFinishedMovie)
        {
            return;
        }

        SkipMovieAndGoNextScene();
    }

    private void SkipMovieAndGoNextScene()
    {

        var gamepad = Gamepad.current;

        if (Input.GetKeyDown((KeyCode.Space)))
        {
            m_isFinishedMovie = true;
            m_videoPlayer.Pause();
            StartCoroutine(GoNextScene());
        }

        if (gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            m_isFinishedMovie = true;
            m_videoPlayer.Pause();
            StartCoroutine(GoNextScene());
        }
    }
    void OnVideoEnd(VideoPlayer vb)
    {
        m_isFinishedMovie = true;
        StartCoroutine(GoNextScene());
    }

    private IEnumerator EnterScene()
    {
        m_videoPlayer.Play();
        m_videoPlayer.Pause();
        yield return StartCoroutine(m_fadeEffect.Fade(false));
        m_videoPlayer.Play();
    }

    private IEnumerator GoNextScene()
    {
        m_videoPlayer.Pause();
        yield return StartCoroutine(m_fadeEffect.Fade(true));
        SceneManager.LoadScene(m_nextScene);
    }

}
