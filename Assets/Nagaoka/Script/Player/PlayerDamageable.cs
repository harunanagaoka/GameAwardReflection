using UnityEngine;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerDamageable : Damageable
{
    private PlayerEvents m_playerEvents;

    protected override void Start()
    {
        base.Start();
        m_playerEvents = GetComponent<PlayerEvents>();
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
