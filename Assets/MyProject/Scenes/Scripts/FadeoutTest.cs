using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public int nextSceneIndex = 0;  // 遷移先のシーンインデックス
    private bool isTransitioning = false;
    private static GameObject mainCanvasInstance;  // MainのCanvasのインスタンス
    public GameObject[] mainUI;

    [Tooltip("画像描画順（CanvasのSorting Order）")]
    [SerializeField]
    public int ImagePriority = 0;

    [System.Serializable]
    public class SceneWaitSetting
    {
       [Tooltip("フェード用画像")]
        public Image fadeImage;
        [Tooltip("フェード時間")]
        public float fadeDuration = 1f;
        [Tooltip("シーン名")]
        public GameScene scene;
        [Tooltip("暗転前の待ち時間")]
        public float waitBeforeFadeOut = 2f;
        [Tooltip("明転前(暗転中)の待ち時間")]
        public float waitBeforeFadeIn = 2f;
    }

    [SerializeField][Tooltip("シーンごとのフェード設定のリスト")]
    public List<SceneWaitSetting> sceneWaitSettings = new List<SceneWaitSetting>();

    private SceneWaitSetting GetWaitSetting(GameScene scene)
    {
        // リストから指定されたシーンに対応する設定を探して返す
        return sceneWaitSettings.Find(s => s.scene == scene);
    }

    void Awake()
    { 
        //必ずフェードの画像が最前面に来るように
        var canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = ImagePriority;
        }

        // MainシーンのCanvasのみDontDestroyOnLoadにする
        if (mainCanvasInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            mainCanvasInstance = gameObject;  // このオブジェクトを保存
        }
    }

    void Start()
    {
        // シーンごとのフェード用画像をすべて透明に初期化
        foreach (var setting in sceneWaitSettings)
        {
            if (setting.fadeImage != null)
            {
                setting.fadeImage.color = new Color(0, 0, 0, 0); // 黒・透明
            }
        }
    }

    void Update()
    {
        // デバッグ用：スペースキーで次のシーンに遷移
        // DebugNextScene();
    }

    public void StartSceneTransition(GameScene scene)
    {
        if (!isTransitioning)
        {
            StartCoroutine(SwitchScene(scene));
        }
    }

    // フェードアウトしてシーンを切り替える
    private IEnumerator SwitchScene(GameScene scene)
    {
        var setting = GetWaitSetting(scene);
        if (setting == null)
        {
            Debug.LogWarning("SceneWaitSettingが見つかりません: " + scene);
            yield break;
        }

        // 暗転前の待ち時間
        if (setting.waitBeforeFadeOut > 0f)
            yield return new WaitForSeconds(setting.waitBeforeFadeOut);

        isTransitioning = true;

        // フェードイン（暗転）
        yield return StartCoroutine(Fade(setting.fadeImage, setting.fadeDuration, 1));

        // シーンを切り替えとUI要素削除
        SceneManager.LoadScene(scene.ToString());
        foreach (GameObject uiElement in mainUI)
        {
            uiElement.SetActive(false);
        }

        // シーンが切り替わるまで待つ（次のフレーム）
        yield return null;
        if(nextSceneIndex <= 4) 
        { 
        nextSceneIndex++;
        }
        if(nextSceneIndex >= 5)
        {
            nextSceneIndex = 0;
        }

        // 明転前の待ち時間
        if (setting.waitBeforeFadeIn > 0f)
            yield return new WaitForSeconds(setting.waitBeforeFadeIn);

        // フェードアウト（明転）
        yield return StartCoroutine(Fade(setting.fadeImage, setting.fadeDuration, 0));

        isTransitioning = false;
    }

    // フェード処理
    private IEnumerator Fade(Image fadeImage, float fadeDuration, float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }

    public void BackMain()
    {
        SceneManager.LoadScene("Main");
       // Destroy(mainCanvasInstance);  // 別シーンに移動したらMain-Canvasを削除
        //mainCanvasInstance = null;
        foreach (GameObject uiElement in mainUI)
        {
            uiElement.SetActive(true);
        }
    
    
    }

    public void ResetCanvas()// リザルト終了時にCanvasをリセットするための関数
    {
            nextSceneIndex = 0;
    }

    public void DebugNextScene()
    {
        if(Input.GetKey(KeyCode.Space))
        StartSceneTransition((GameScene)nextSceneIndex);
    }
}
