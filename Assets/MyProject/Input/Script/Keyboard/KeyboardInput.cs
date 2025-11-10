using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    private List<PlayerEvents> m_playerEvents = new List<PlayerEvents>();

    private Dictionary<KeyCode, UnityEvent> m_keyEventMap;
    private Dictionary<KeyCode, UnityEvent> m_keyDownEventMap;

    enum MouseButton
    {
        Left,
        Right
    }

    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (PlayerManager.Instance.Players.Count == 0)
        {
            return;
        }


        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            foreach (var plevent in m_playerEvents)
            {
                plevent.OnAttack?.Invoke();
            }
        }

        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            foreach(var plevent in m_playerEvents){
                plevent.OnDefence?.Invoke();
            }
        }

        if (Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            foreach (var plevent in m_playerEvents)
            {
                plevent.OnDefenceEnd?.Invoke();
            }
        }


        foreach (var pair in m_keyEventMap)
        {
            if (Input.GetKey(pair.Key)) pair.Value?.Invoke();
        }

        foreach (var pair in m_keyDownEventMap)
        {
            if (Input.GetKeyDown(pair.Key)) pair.Value?.Invoke();
        }
    }

    private void Initialize()
    {
        //リスト初期化
        m_playerEvents.Clear();

        foreach (GameObject playerObj in PlayerManager.Instance.Players)
        {
            PlayerEvents playerEvent = playerObj.GetComponent<PlayerEvents>();
            if (playerEvent != null)
            {
                m_playerEvents.Add(playerEvent);
            }
        }


        //キーマップ初期化
        if (PlayerManager.Instance.Players.Count == 0)
        {
            return;
        }

        if (PlayerManager.Instance.Players.Count == 1)
        {
            m_keyEventMap = new Dictionary<KeyCode, UnityEvent>
            {
            { KeyCode.W, m_playerEvents[0].OnMoveForward },
            { KeyCode.S, m_playerEvents[0].OnMoveBackward },
            { KeyCode.D, m_playerEvents[0].OnMoveRight },
            { KeyCode.A, m_playerEvents[0].OnMoveLeft }
            //{ KeyCode.Q, m_playerEvents[0].OnTurnLeft },
            //{ KeyCode.E, m_playerEvents[0].OnTurnRight },
            };


            m_keyDownEventMap = new Dictionary<KeyCode, UnityEvent>
        {
            //ジャンプとか押したときだけ反応するやつ
            //{ KeyCode.Tab, m_playerEvents[0].OnDefence },
            //{ KeyCode.R, m_playerEvents[0].OnAttack }
        };
    }

        //旧　プレイヤー2人対応のなごり
        //if (PlayerManager.Instance.Players.Count == 2)
        //{
        //    m_keyEventMap = new Dictionary<KeyCode, UnityEvent>
        //{
        //    { KeyCode.W, m_playerEvents[0].OnMoveForward },
        //    { KeyCode.S, m_playerEvents[0].OnMoveBackward },
        //    { KeyCode.D, m_playerEvents[0].OnMoveRight },
        //    { KeyCode.A, m_playerEvents[0].OnMoveLeft },
        //    { KeyCode.Q, m_playerEvents[0].OnTurnLeft },
        //    { KeyCode.E, m_playerEvents[0].OnTurnRight },

        //    { KeyCode.I, m_playerEvents[1].OnMoveForward },
        //    { KeyCode.K, m_playerEvents[1].OnMoveBackward },
        //    { KeyCode.L, m_playerEvents[1].OnMoveRight },
        //    { KeyCode.J, m_playerEvents[1].OnMoveLeft },
        //    { KeyCode.U, m_playerEvents[1].OnTurnLeft },
        //    { KeyCode.O, m_playerEvents[1].OnTurnRight }
        //};

        //    m_keyDownEventMap = new Dictionary<KeyCode, UnityEvent>
        //{
        //    //ジャンプとか押したときだけ反応するやつ
        //    { KeyCode.Tab, m_playerEvents[0].OnDefence },
        //     { KeyCode.R, m_playerEvents[0].OnAttack },
        //    { KeyCode.Y, m_playerEvents[1].OnDefence },
        //     { KeyCode.P, m_playerEvents[1].OnAttack }
        //};

        //}

    }
}
