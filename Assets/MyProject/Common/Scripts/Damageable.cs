using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected float m_maxHitInterval = 0;

    private float m_hitInterval = 0;
    private bool m_isDamageInterval = false;

    protected abstract void OnDamagePenaltyEvent(float damage);

    protected abstract void OnDamageEvent();

    protected virtual bool CanTakeDamageCore =>
    !m_isDamageInterval;

    protected virtual void Start()
    {
        //共通の初期化あれば書いてね
    }

    protected virtual void Awake()
    {

    }

    protected void Update()
    {
        ProcessInterval();
    }

    public void TakeDamage(float damage)
    {
        if (!CanTakeDamageCore) return;

        OnDamagePenaltyEvent(damage);
        OnDamageEvent();
        SetInterval();

        //if (m_hitPoint <= 0)
        //{
        //    OnDeathEvent();//HPの管理を継承先に任せたためコメントアウト
        //}
    }

    private void SetInterval()
    {
        m_hitInterval = m_maxHitInterval;
        m_isDamageInterval = true;
    }

    private void ProcessInterval()
    {
        if (m_hitInterval <= 0)
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
