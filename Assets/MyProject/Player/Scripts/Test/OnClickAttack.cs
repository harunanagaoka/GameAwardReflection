using UnityEngine;

public class OnClickAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_atkPrefab;

    private PlayerEvents m_event;

    private PlayerBlownAway m_blownAway;

    private ComboManager m_comboManager;

    void Start()
    {
        m_event = GetComponent<PlayerEvents>();
        m_blownAway = GetComponent<PlayerBlownAway>();
        m_comboManager = GetComponent<ComboManager>();
        m_event.OnDefenceEnd.AddListener(BlowCancelAttack);

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
    private void BlowCancelAttack()
    {
        if (m_blownAway.IsBlownAway)
        {
            InitAttack();
            m_blownAway.StopBlownAway();
        }
    }

    private void InitAttack()
    {
        GameObject atk = Instantiate(m_atkPrefab, transform.position, Quaternion.identity, this.transform);
        PlayerAttackCollider atkCollider = atk.GetComponent<PlayerAttackCollider>();
        float damage = m_comboManager.GetComboDamage();
        atkCollider.SetDamage(damage);
    }
}
//吹き飛び状態の時にDefenceEndが入力されたらOnBlownAwayCanceled?.Invoke()