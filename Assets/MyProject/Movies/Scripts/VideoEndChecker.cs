using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndChecker : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private string m_nextScene;

    void Start()
    {
        // 動画終了時に呼ばれるイベントを登録
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("動画が終了しました！");
        SceneManager.LoadScene(m_nextScene);
    }
}

