using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected int m_hitPoint = 0;
    [SerializeField] protected float m_maxHitInterval = 0;

    private float m_hitInterval = 0;
    private bool m_isDamageable = true;

    protected abstract void OnDamageEvent();
    protected abstract void OnDeathEvent();

    protected virtual void Start()
    {
        //‹¤’Ê‚Ì‰Šú‰»‚ ‚ê‚Î‘‚¢‚Ä‚Ë
    }

    private void Update()
    {
        ProcessInterval();
    }

    public void TakeDamage(int damage)
    {
        if (m_hitPoint <= 0 || !m_isDamageable) return;

        m_hitPoint -= damage;
        OnDamageEvent();
        ResetInterval();

        if (m_hitPoint <= 0)
        {
            OnDeathEvent();
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
