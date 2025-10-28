//AI産スクリプトです
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager m_instance;

    private List<GameObject> players = new List<GameObject>();

    public int PlayerCount => players.Count;

    public IReadOnlyList<GameObject> Players => players;

    public static PlayerManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject obj = new GameObject("PlayerManager");
                m_instance = obj.AddComponent<PlayerManager>();
                DontDestroyOnLoad(obj);
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        // 既にインスタンスが存在する場合は破棄
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPlayer(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("プレイヤーのゲームオブジェクトがnullです");
            return;
        }

        if (players.Contains(player))
        {
            Debug.LogWarning($"{player.name}は既に登録されています");
            return;
        }

        players.Add(player);
        Debug.Log($"プレイヤー登録: {player.name} (現在 {PlayerCount}人)");
    }

    public void UnregisterPlayer(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("登録解除しようとしたプレイヤーがnullです");
            return;
        }

        if (players.Remove(player))
        {
            Debug.Log($"プレイヤー登録解除: {player.name} (現在 {PlayerCount}人)");
        }
        else
        {
            Debug.LogWarning($"{player.name}は登録されていません");
        }
    }

    public GameObject GetPlayer(int index)
    {
        if (index < 0 || index >= players.Count)
        {
            Debug.LogWarning($"インデックス{index}は範囲外です（0～{players.Count - 1}）");
            return null;
        }
        return players[index];
    }
    public void ClearAllPlayers()
    {
        players.Clear();
        Debug.Log("すべてのプレイヤーをクリアしました");
    }

    /// <summary>
    /// ゲームループ時の初期化処理
    /// 登録されているすべてのプレイヤーオブジェクトを破棄してリストをクリア
    /// </summary>
    public void ResetManager()
    {
        // リストのコピーを作成（破棄中にリストが変更されるのを防ぐ）
        List<GameObject> playersCopy = new List<GameObject>(players);

        foreach (GameObject player in playersCopy)
        {
            if (player != null)
            {
                Destroy(player);
            }
        }

        players.Clear();
        Debug.Log("PlayerManagerをリセットしました");
    }

    /// <summary>
    /// ゲームループ時の初期化処理（破棄せずにリストのみクリア）
    /// プレイヤーオブジェクトは別の場所で管理する場合に使用
    /// </summary>
    public void ResetManagerWithoutDestroy()
    {
        players.Clear();
        Debug.Log("PlayerManagerをリセットしました（オブジェクトは破棄せず）");
    }
}
