using UnityEngine;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyDamageable : MonoBehaviour
{
    [SerializeField]
    private EnemyData m_enemyData;

    private EnemyEvents m_enemyEvents;

    [SerializeField]
    private int m_hitPoint = 0;

    [SerializeField]
    private float m_maxHitInterval = 0;

    private float m_hitInterval = 0;

    private bool m_isDamageable = true;

    void Start()
    {
        m_enemyEvents = GetComponent<EnemyEvents>();
        m_hitPoint = m_enemyData.HP;
    }

    private void Update()
    {
        ProcessInterval();
    }

    public void TakeDamage(int damage)
    {
        if (m_hitPoint <= 0 || !m_isDamageable)
        {
            return;
        }

        m_hitPoint -= damage;
        m_enemyEvents.OnDamage?.Invoke();
        ResetInterval();

        if (m_hitPoint <= 0)
        {
            m_enemyEvents.OnDeath?.Invoke();
        }
    }

    private void ResetInterval()
    {
        m_hitInterval = m_maxHitInterval;
        m_isDamageable = false;
    }

    private void ProcessInterval()
    {
        if (m_hitInterval < 0)
        {
            return;
        }

        m_hitInterval -= Time.deltaTime;

        if (m_hitInterval <= 0)
        {
            m_isDamageable = true;
        }
    }
}
