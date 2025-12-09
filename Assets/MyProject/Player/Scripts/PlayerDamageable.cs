using UnityEngine;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerDamageable : Damageable
{
    private PlayerEvents m_playerEvents;

    private bool m_isDefending = false;

    protected override bool CanTakeDamageCore =>
    base.CanTakeDamageCore && !m_isDefending;

    protected override void Start()
    {
        base.Start();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isDefending = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isDefending = false);
    }

    protected override void OnDamageEvent()
    {
        m_playerEvents.OnDamage?.Invoke();
    }

    protected override void OnDeathEvent()
    {
        m_playerEvents.OnDeath?.Invoke();
    }
}
