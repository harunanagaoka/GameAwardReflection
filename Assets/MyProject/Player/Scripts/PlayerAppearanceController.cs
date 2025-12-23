using UnityEngine;

public class PlayerAppearanceController : MonoBehaviour
{
    [SerializeField, Tooltip("通常の人型モデル")]
    private GameObject m_humanModel;

    [SerializeField, Tooltip("攻撃中に使うモデル")]
    private GameObject m_attackModel;

    [SerializeField, Tooltip("防御時 / 吹き飛ばし時に使う球状モデル")]
    private GameObject m_ballModel;

    [SerializeField, Tooltip("攻撃モデルを表示する時間（秒）")]
    private float m_attackDisplayTime = 0.15f;

    private PlayerEvents m_playerEvents;

    private bool m_isDefending = false;
    private bool m_isBlownAway = false;

    // ★ 攻撃残り時間
    private float m_attackTimer = 0f;

    void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        if (m_playerEvents == null)
        {
            Debug.LogWarning($"{nameof(PlayerAppearanceController)}: PlayerEvents が見つかりません。");
            return;
        }

        m_playerEvents.OnDefence.AddListener(() => m_isDefending = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isDefending = false);

        m_playerEvents.OnBlownAway.AddListener(() => m_isBlownAway = true);
        m_playerEvents.OnBlownAwayCanceled.AddListener(() => m_isBlownAway = false);
        m_playerEvents.OnBlownAwayEnd.AddListener(() => m_isBlownAway = false);

        m_playerEvents.OnBlownAwayCanceled.AddListener(OnAttack);

        UpdateAppearance();
    }

    void OnDestroy()
    {
        if (m_playerEvents == null) return;

        m_playerEvents.OnAttack.RemoveListener(OnAttack);
    }

    void Update()
    {
        if (m_attackTimer > 0f)
        {
            m_attackTimer -= Time.deltaTime;
            if (m_attackTimer < 0f)
                m_attackTimer = 0f;
        }

        UpdateAppearance();
    }

    private void OnAttack()
    {
        // 攻撃が来たら「残り時間を上書き」
        m_attackTimer = m_attackDisplayTime;
    }

    private void UpdateAppearance()
    {
        bool isAttacking = m_attackTimer > 0f;

        // 優先順位:
        // 球体 > 攻撃 > 通常
        bool showBall = m_isBlownAway || m_isDefending;
        bool showAttack = !showBall && isAttacking;
        bool showHuman = !showBall && !showAttack;

        SetActiveSafe(m_ballModel, showBall);
        SetActiveSafe(m_attackModel, showAttack);
        SetActiveSafe(m_humanModel, showHuman);
    }

    private void SetActiveSafe(GameObject obj, bool active)
    {
        if (obj == null) return;
        if (obj.activeSelf == active) return;
        obj.SetActive(active);
    }
}
