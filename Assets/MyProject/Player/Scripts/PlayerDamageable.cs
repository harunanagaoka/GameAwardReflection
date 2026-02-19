using UnityEngine;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerDamageable : Damageable
{
    [SerializeField]
    private PlayerData m_playerData;

    private PlayerEvents m_playerEvents;

    private bool m_isDefending = false;

    private float m_currentHitPoint;

    private bool m_isStun = false;

    public float HitPoint => m_currentHitPoint;

    public float HitPointRate => m_currentHitPoint / m_playerData.MaxHitPoint;

    private MainGameTimer m_mainGameTimer;

    protected override bool CanTakeDamageCore =>
    base.CanTakeDamageCore && !m_isDefending;

    protected override void Start()
    {
        base.Start();

        m_mainGameTimer = FindAnyObjectByType<MainGameTimer>();

        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isDefending = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isDefending = false);
        m_playerEvents.OnStun.AddListener(() => m_isStun = true);
        m_playerEvents.OnStunEnd.AddListener(()=>  m_isStun = false);
        m_playerEvents.OnDamagePenalty.AddListener(TimePenalty);
        m_playerEvents.OnDamagePenalty.AddListener(DecreaseHP);
        m_maxHitInterval = m_playerData.DamageInterval;

        m_currentHitPoint = m_playerData.MaxHitPoint;
    }

    protected override void OnDamageEvent()
    {
        m_playerEvents.OnDamage?.Invoke();
    }

    protected override void OnDamagePenaltyEvent(float damage)
    {
        m_playerEvents.OnDamagePenalty?.Invoke(damage);
    }

    private void DecreaseHP(float damage)
    {
        m_currentHitPoint -= damage;
    }

    //protected override void OnDeathEvent()
    //{
    //    m_playerEvents.OnDeath?.Invoke();//HPの概念がなくなったためコメントアウト
    //}

    private void TimePenalty(float penalty)
    {
        m_mainGameTimer.DecreseTime(penalty);
    }
}
