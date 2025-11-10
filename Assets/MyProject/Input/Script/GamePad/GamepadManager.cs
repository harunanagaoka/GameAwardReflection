//AI産スクリプトです
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadManager : MonoBehaviour
{
    private static GamepadManager m_instance;

    // ゲームパッドのdeviceId → プレイヤーインデックス
    private Dictionary<int, int> m_gamepadToPlayer = new Dictionary<int, int>();

    // プレイヤーインデックス → Gamepad
    private Dictionary<int, Gamepad> m_playerToGamepad = new Dictionary<int, Gamepad>();

    public static GamepadManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject obj = new GameObject("GamepadManager");
                m_instance = obj.AddComponent<GamepadManager>();
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

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    /// <summary>
    /// プレイヤーとゲームパッドを自動で割り当て
    /// PlayerManagerに登録されているプレイヤー数に応じて割り当てる
    /// </summary>
    public void RegisterPlayers()
    {
        m_gamepadToPlayer.Clear();
        m_playerToGamepad.Clear();

        int playerCount = PlayerManager.Instance.PlayerCount;
        int gamepadCount = Gamepad.all.Count;

        if (gamepadCount == 0)
        {
            Debug.LogWarning("ゲームパッドが接続されていません");
            return;
        }

        // プレイヤー数とゲームパッド数の少ない方まで割り当て
        int assignCount = Mathf.Min(playerCount, gamepadCount);

        for (int i = 0; i < assignCount; i++)
        {
            Gamepad gamepad = Gamepad.all[i];
            m_gamepadToPlayer[gamepad.deviceId] = i;
            m_playerToGamepad[i] = gamepad;

            Debug.Log($"Player {i + 1} ← {gamepad.name} (DeviceID: {gamepad.deviceId})");
        }

        if (gamepadCount < playerCount)
        {
            Debug.LogWarning($"プレイヤー数({playerCount})に対してゲームパッド数({gamepadCount})が不足しています");
        }
    }

    /// <summary>
    /// 特定のプレイヤーに特定のゲームパッドを割り当て
    /// </summary>
    /// <param name="playerIndex">プレイヤーインデックス（0始まり）</param>
    /// <param name="gamepadIndex">ゲームパッドインデックス（0始まり）</param>
    public void AssignGamepadToPlayer(int playerIndex, int gamepadIndex)
    {
        if (gamepadIndex < 0 || gamepadIndex >= Gamepad.all.Count)
        {
            Debug.LogWarning($"ゲームパッドインデックス{gamepadIndex}は範囲外です");
            return;
        }

        if (playerIndex < 0 || playerIndex >= PlayerManager.Instance.PlayerCount)
        {
            Debug.LogWarning($"プレイヤーインデックス{playerIndex}は範囲外です");
            return;
        }

        Gamepad gamepad = Gamepad.all[gamepadIndex];
        m_gamepadToPlayer[gamepad.deviceId] = playerIndex;
        m_playerToGamepad[playerIndex] = gamepad;

        Debug.Log($"Player {playerIndex + 1} ← {gamepad.name} を手動割り当て");
    }

    /// <summary>
    /// ゲームパッドのdeviceIdからプレイヤーインデックスを取得
    /// </summary>
    public int GetPlayerIndex(int deviceId)
    {
        if (m_gamepadToPlayer.TryGetValue(deviceId, out int playerIndex))
        {
            return playerIndex;
        }
        return -1; // 見つからない場合
    }

    /// <summary>
    /// プレイヤーインデックスから割り当てられたGamepadを取得
    /// </summary>
    public Gamepad GetGamepad(int playerIndex)
    {
        if (m_playerToGamepad.TryGetValue(playerIndex, out Gamepad gamepad))
        {
            return gamepad;
        }
        return null;
    }

    /// <summary>
    /// 指定したプレイヤーにゲームパッドが割り当てられているか
    /// </summary>
    public bool HasGamepad(int playerIndex)
    {
        return m_playerToGamepad.ContainsKey(playerIndex);
    }

    /// <summary>
    /// すべての割り当てをクリア
    /// </summary>
    public void ClearAllAssignments()
    {
        m_gamepadToPlayer.Clear();
        m_playerToGamepad.Clear();
        Debug.Log("ゲームパッドの割り当てをクリアしました");
    }

    /// <summary>
    /// デバイス変更時のイベント処理
    /// </summary>
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad gamepad)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log($"ゲームパッド接続: {gamepad.name}");
                    // 自動再割り当て
                    RegisterPlayers();
                    break;

                case InputDeviceChange.Removed:
                    Debug.Log($"ゲームパッド切断: {gamepad.name}");
                    RemoveGamepad(gamepad);
                    break;

                case InputDeviceChange.Reconnected:
                    Debug.Log($"ゲームパッド再接続: {gamepad.name}");
                    RegisterPlayers();
                    break;
            }
        }
    }

    /// <summary>
    /// 切断されたゲームパッドの割り当てを削除
    /// </summary>
    private void RemoveGamepad(Gamepad gamepad)
    {
        if (m_gamepadToPlayer.TryGetValue(gamepad.deviceId, out int playerIndex))
        {
            m_gamepadToPlayer.Remove(gamepad.deviceId);
            m_playerToGamepad.Remove(playerIndex);
            Debug.Log($"Player {playerIndex + 1} のゲームパッド割り当てを解除しました");
        }
    }

}

