using UnityEngine;
using UnityEngine.SceneManagement; // Unityのシーン管理機能を使うため

// シーン名を定義する列挙型
public enum GameScene
{
    Title,
    MainGame,
    Result
}

// シーン管理を行うシングルトンクラス
public class SceneController : MonoBehaviour
{
    // 唯一のインスタンスを保持する静的変数
    private static SceneController _instance;

    // 外部からアクセスできるプロパティ
    public static SceneController Instance
    {
        get
        {
            return _instance;
        }
    }

    // 現在のシーンを保持する変数（外部から読み取り可能）
    public GameScene CurrentScene { get; private set; }

    private void Awake()
    {
        // シングルトンの初期化
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも残す
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 重複を防ぐ
        }
    }

    // シーンを切り替える関数
    public void ChangeScene(GameScene scene)
    {
        CurrentScene = scene; // 現在のシーンを更新
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString()); // シーンをロード
    }
}



