using UnityEngine;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyDamageable : Damageable
{
    [SerializeField]
    private EnemyData m_enemyData;

    private EnemyEvents m_enemyEvents;

    protected override void Start()
    {
        base.Start();
        m_enemyEvents = GetComponent<EnemyEvents>();
        m_hitPoint = m_enemyData.HP;
    }

    protected override void OnDamageEvent()
    {
        m_enemyEvents.OnDamage?.Invoke();
    }

    protected override void OnDeathEvent()
    {
        m_enemyEvents.OnDeath?.Invoke();
    }
}
