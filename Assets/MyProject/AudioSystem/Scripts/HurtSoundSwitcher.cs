using Unity.VisualScripting;
using UnityEngine;

public class HurtSoundSwitcher : MonoBehaviour
{
    private SEManager m_seManager;
    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_seManager = Object.FindFirstObjectByType<SEManager>();
        m_playerEvents = GetComponent<PlayerEvents>();
        if (m_playerEvents != null)
        {
            m_playerEvents.OnDamage.AddListener(OnDamageReceived);
        }
    }

    // 引数なしに修正
    public void OnDamageReceived()
    {
        // 共通のダメージSEを鳴らす（例: PlayerNumb）
        if (m_seManager != null)
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.BossPunchHit);
        }
    }
}
