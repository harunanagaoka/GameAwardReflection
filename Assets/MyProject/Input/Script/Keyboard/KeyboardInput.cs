//AI産スクリプト

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    private SEManager m_seManager;

    private List<PlayerEvents> m_playerEvents = new List<PlayerEvents>();

    private bool m_wasInit = false;

    // 新: 柔軟なバインディングリスト
    private enum KeyEventType { Hold, Down, Up }

    [System.Serializable]
    private class KeyBinding
    {
        // 外部から変更できない読み取り専用プロパティに変更
        public int PlayerIndex { get; }
        public KeyCode Key { get; }
        public KeyEventType Type { get; }
        public UnityEvent Event { get; private set; }
        public bool IsMovement { get; }

        public KeyBinding(int playerIndex, KeyCode key, KeyEventType type, UnityEvent unityEvent, bool isMovement = false)
        {
            PlayerIndex = playerIndex;
            Key = key;
            Type = type;
            Event = unityEvent;
            IsMovement = isMovement;
        }

        // イベントを差し替えたい場合
        public void SetEvent(UnityEvent unityEvent) => Event = unityEvent;
    }

    private List<KeyBinding> m_bindings = new List<KeyBinding>();

    // OnStop 判定用
    private List<bool> m_wasMoving = new List<bool>();
    private List<List<KeyCode>> m_moveKeysPerPlayer = new List<List<KeyCode>>();

    enum MouseButton
    {
        Left,
        Right
    }

    private void Awake()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnResisterPlayer += Initialize;
        }// Initialize();
        m_seManager = Object.FindFirstObjectByType<SEManager>();
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnResisterPlayer -= Initialize;
        }
    }

    void Update()
    {
        if (!m_wasInit)
        {
            return;
        }

            if (PlayerManager.Instance.Players.Count == 0)
        {
            return;
        }

        // マウス操作（攻撃・防御）
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            foreach (var plevent in m_playerEvents)
            {
                plevent.OnAttack?.Invoke();
            }
        }

        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            foreach (var plevent in m_playerEvents)
            {
                plevent.OnDefence?.Invoke();
            }

            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.PlayerGard);

        }

        if (Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            foreach (var plevent in m_playerEvents)
            {
                plevent.OnDefenceEnd?.Invoke();
            }
        }

        for (int i = 0; i < m_bindings.Count; i++)
        {
            var b = m_bindings[i];
            switch (b.Type)
            {
                case KeyEventType.Hold:
                    if (Input.GetKey(b.Key)) b.Event?.Invoke();
                    break;
                case KeyEventType.Down:
                    if (Input.GetKeyDown(b.Key)) b.Event?.Invoke();
                    break;
                case KeyEventType.Up:
                    if (Input.GetKeyUp(b.Key)) b.Event?.Invoke();
                    break;
            }
        }

        // OnStop / OnStartMove 判定
        for (int i = 0; i < m_playerEvents.Count; i++)
        {
            while (m_wasMoving.Count <= i) m_wasMoving.Add(false);
            while (m_moveKeysPerPlayer.Count <= i) m_moveKeysPerPlayer.Add(new List<KeyCode>());

            bool isMoving = false;
            var keys = m_moveKeysPerPlayer[i];
            for (int k = 0; k < keys.Count; k++)
            {
                if (Input.GetKey(keys[k]))
                {
                    isMoving = true;
                    break;
                }
            }

            // 前フレームは移動していなかった && 今フレームは移動している
            if (!m_wasMoving[i] && isMoving)
            {
                m_playerEvents[i].OnStartMove?.Invoke();
            }

            // 前フレームは移動していた && 今フレームは移動していない
            if (m_wasMoving[i] && !isMoving)
            {
                m_playerEvents[i].OnStop?.Invoke();
            }

            m_wasMoving[i] = isMoving;
        }
    }

    public void Initialize()
    {
        // プレイヤーリスト初期化
        m_playerEvents.Clear();
        m_bindings.Clear();
        m_wasMoving.Clear();
        m_moveKeysPerPlayer.Clear();

        foreach (GameObject playerObj in PlayerManager.Instance.Players)
        {
            PlayerEvents playerEvent = playerObj.GetComponent<PlayerEvents>();
            if (playerEvent != null)
            {
                m_playerEvents.Add(playerEvent);
                m_wasMoving.Add(false);
                m_moveKeysPerPlayer.Add(new List<KeyCode>());
            }
        }

        // キーマップ初期化
        if (PlayerManager.Instance.Players.Count == 0)
        {
            return;
        }

        if (PlayerManager.Instance.Players.Count == 1)
        {
            // Hold: 押している間発火 (移動)
            AddBinding(0, KeyCode.W, KeyEventType.Hold, m_playerEvents[0].OnMoveForward, isMovement: true);
            AddBinding(0, KeyCode.S, KeyEventType.Hold, m_playerEvents[0].OnMoveBackward, isMovement: true);
            AddBinding(0, KeyCode.D, KeyEventType.Hold, m_playerEvents[0].OnMoveRight, isMovement: true);
            AddBinding(0, KeyCode.A, KeyEventType.Hold, m_playerEvents[0].OnMoveLeft, isMovement: true);
        }

        m_wasInit = true;
    }

    private void AddBinding(int playerIndex, KeyCode key, KeyEventType type, UnityEvent unityEvent, bool isMovement = false)
    {
        if (playerIndex < 0) return;

        while (m_moveKeysPerPlayer.Count <= playerIndex)
        {
            m_moveKeysPerPlayer.Add(new List<KeyCode>());
        }

        m_bindings.Add(new KeyBinding(playerIndex, key, type, unityEvent, isMovement));

        if (isMovement)
        {
            m_moveKeysPerPlayer[playerIndex].Add(key);
        }
    }

    // 再初期化
    public void Reinitialize()
    {
        Initialize();
    }
}