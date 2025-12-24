using UnityEngine;
using UnityEngine.InputSystem;

public class OnClickAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_atkPrefab;

    private PlayerEvents m_event;

    private PlayerBlownAway m_blownAway;

    private Gamepad m_gamepad;

    private bool isInited = false;

    void Start()
    {
        m_event = GetComponent<PlayerEvents>();
        m_blownAway = GetComponent<PlayerBlownAway>();
        m_event.OnDefenceEnd.AddListener(BlowCancelAttack);
        //m_event.OnBlownAwayCanceled.AddListener(BlowCancelAttack);
        
    }
    private void Update()
    {
        //if (!isInited)
        //{
        //    m_gamepad = GamepadManager.Instance.GetGamepad(0);
        //    isInited = true;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{

        //    if (m_blownAway.IsBlownAway)
        //    {
        //        m_event.OnBlownAwayCanceled?.Invoke();
        //       // m_event.OnAttack?.Invoke();
        //    }

        //}

        //if(m_gamepad != null)
        //{
        //    if (m_gamepad.bButton.wasPressedThisFrame)
        //    {

        //        if (m_blownAway.IsBlownAway)
        //        {
        //            m_event.OnBlownAwayCanceled?.Invoke();
        //            // m_event.OnAttack?.Invoke();
        //        }
        //    }
        //}

        ////isBlownAwayかつクリックしたら攻撃isBlownAway解除
        ////攻撃演出
    }

    private void DefenceEndAttack()
    {

    }

    private void BlowCancelAttack()
    {
        if (m_blownAway.IsBlownAway)
        {
            m_blownAway.StopBlownAway();
            Instantiate(m_atkPrefab, transform.position, Quaternion.identity, this.transform);
        }
    }
}
//吹き飛び状態の時にDefenceEndが入力されたらOnBlownAwayCanceled?.Invoke()