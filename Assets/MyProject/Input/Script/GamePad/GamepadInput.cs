using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInput : MonoBehaviour
{
    private List<PlayerEvents> m_playerEvents = new List<PlayerEvents>();

    private List<bool> m_wasMoving = new List<bool>();//OnStop呼び出し用

    private float m_stickDeadzone = 0.1f;

    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        // 各プレイヤーの入力処理
        for (int i = 0; i < m_playerEvents.Count; i++)
        {
            // GamepadManagerから該当プレイヤーのGamepadを取得
            Gamepad gamepad = GamepadManager.Instance.GetGamepad(i);

            if (gamepad != null)
            {
                ProcessPlayerInput(i, gamepad);
            }
        }
    }

    private void ProcessPlayerInput(int playerIndex, Gamepad gamepad)
    {
        if (playerIndex >= m_playerEvents.Count)
            return;

        PlayerEvents playerEvent = m_playerEvents[playerIndex];

        // 左スティック（移動）
        Vector2 leftStick = gamepad.leftStick.ReadValue();

        bool isMoving = Mathf.Abs(leftStick.x) > m_stickDeadzone || Mathf.Abs(leftStick.y) > m_stickDeadzone;

        while (m_wasMoving.Count <= playerIndex)
        {
            m_wasMoving.Add(false);
        }
            
        bool wasMoving = m_wasMoving[playerIndex];

        if (!wasMoving && isMoving)
        {
            playerEvent.OnStartMove?.Invoke();
        }


        if (isMoving)
        {
            // 前後移動
            if (leftStick.y > m_stickDeadzone)
            {
                playerEvent.OnMoveForward?.Invoke();
            }
            else if (leftStick.y < -m_stickDeadzone)
            {
                playerEvent.OnMoveBackward?.Invoke();
            }

            // 左右移動
            if (leftStick.x > m_stickDeadzone)
            {
                playerEvent.OnMoveRight?.Invoke();
            }
            else if (leftStick.x < -m_stickDeadzone)
            {
                playerEvent.OnMoveLeft?.Invoke();
            }
        }
        else
        {
            // 以前は移動していて、今は移動が終わった場合
            if (wasMoving)
            {
                playerEvent.OnStop?.Invoke();
            }
        }
            //// 右スティック（回転） 
            //Vector2 rightStick = gamepad.rightStick.ReadValue();

            //if (rightStick.x > m_stickDeadzone)
            //{
            //    playerEvent.OnTurnRight?.Invoke();
            //}
            //else if (rightStick.x < -m_stickDeadzone)
            //{
            //    playerEvent.OnTurnLeft?.Invoke();
            //}

            //防御
        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            playerEvent.OnDefence?.Invoke();
        }
        if (gamepad.rightTrigger.wasReleasedThisFrame)
        {
            playerEvent.OnDefenceEnd?.Invoke();
        }

        //攻撃
        if (gamepad.bButton.wasPressedThisFrame)
        {
            playerEvent.OnAttack?.Invoke();
        }

        m_wasMoving[playerIndex] = isMoving;
    }

    private void Initialize()
    {
        m_playerEvents.Clear();

        foreach (GameObject playerObj in PlayerManager.Instance.Players)
        {
            PlayerEvents playerEvent = playerObj.GetComponent<PlayerEvents>();
            if (playerEvent != null)
            {
                m_playerEvents.Add(playerEvent);
            }
        }

        // ゲームパッド割り当て
        GamepadManager.Instance.RegisterPlayers();
    }

    // 再初期化
    public void Reinitialize()
    {
        Initialize();
    }
}
