using UnityEngine;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerDamageable : MonoBehaviour
{
    private PlayerEvents m_playerEvents;

    [SerializeField]
    private int m_hitPoint = 0;

    [SerializeField]
    private float m_maxHitInterval = 0;

    private float m_hitInterval = 0;

    private bool m_isDamageable = true;

    void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
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
        m_playerEvents.OnDamage?.Invoke();
        ResetInterval();

        if (m_hitPoint <= 0)
        {
            m_playerEvents.OnDeath?.Invoke();
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
