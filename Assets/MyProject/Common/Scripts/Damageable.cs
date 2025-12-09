using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected int m_hitPoint = 0;
    [SerializeField] protected float m_maxHitInterval = 0;

    private float m_hitInterval = 0;
    private bool m_isDamageInterval = false;

    protected abstract void OnDamageEvent();
    protected abstract void OnDeathEvent();

    public int HitPoint => m_hitPoint;

    protected virtual bool CanTakeDamageCore =>
    !m_isDamageInterval &&
    m_hitPoint > 0;

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
        if (!CanTakeDamageCore) return;

        m_hitPoint -= damage;
        OnDamageEvent();
        SetInterval();

        if (m_hitPoint <= 0)
        {
            OnDeathEvent();
        }
    }

    private void SetInterval()
    {
        m_hitInterval = m_maxHitInterval;
        m_isDamageInterval = true;
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
            m_isDamageInterval = false;
        }
    }
}
