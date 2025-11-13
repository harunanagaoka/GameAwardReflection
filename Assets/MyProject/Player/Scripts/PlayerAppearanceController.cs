//AI産スクリプト
using UnityEngine;

public class PlayerAppearanceController : MonoBehaviour
{
    [SerializeField,Tooltip("通常の人型モデル")]
    private GameObject m_humanModel;

    [SerializeField, Tooltip("防御時 / 吹き飛ばし時に使う球状モデル")]
    private GameObject m_ballModel;

    private PlayerEvents m_playerEvents;

    // 現在の入力/状態を保持
    private bool m_isDefending = false;
    private bool m_isBlownAway = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        if (m_playerEvents == null)
        {
            Debug.LogWarning($"{nameof(PlayerAppearanceController)}: PlayerEvents が見つかりません。コンポーネントを同じ GameObject にアタッチしてください。");
            return;
        }

        m_playerEvents.OnDefence.AddListener(OnDefenceStart);
        m_playerEvents.OnDefenceEnd.AddListener(OnDefenceEnd);
        m_playerEvents.OnBlownAway.AddListener(OnBlownAwayStart);
        m_playerEvents.OnBlownAwayEnd.AddListener(OnBlownAwayEnd);

        UpdateAppearance();
    }

    void OnDestroy()
    {
        if (m_playerEvents == null) return;
        m_playerEvents.OnDefence.RemoveListener(OnDefenceStart);
        m_playerEvents.OnDefenceEnd.RemoveListener(OnDefenceEnd);
        m_playerEvents.OnBlownAway.RemoveListener(OnBlownAwayStart);
        m_playerEvents.OnBlownAwayEnd.RemoveListener(OnBlownAwayEnd);
    }

    private void OnDefenceStart()
    {
        m_isDefending = true;
        UpdateAppearance();
    }

    private void OnDefenceEnd()
    {
        m_isDefending = false;
        UpdateAppearance();
    }

    private void OnBlownAwayStart()
    {
        m_isBlownAway = true;
        UpdateAppearance();
    }

    private void OnBlownAwayEnd()
    {
        m_isBlownAway = false;
        UpdateAppearance();
    }

    // 球にする条件:
    // - BlownAway 中 もしくは Defending 中
    private void UpdateAppearance()
    {
        bool showBall = m_isBlownAway || m_isDefending;
        SetActiveSafe(m_humanModel, !showBall);
        SetActiveSafe(m_ballModel, showBall);
    }

    private void SetActiveSafe(GameObject safe, bool active)
    {
        if (safe == null) {
            return; 
        }

        if (safe.activeSelf == active) {
            return; 
        }

        safe.SetActive(active);
    }
}
