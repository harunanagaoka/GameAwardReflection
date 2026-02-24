using UnityEngine;

public class PlayerBlowAwayAttack : MonoBehaviour
{
    [SerializeField]
    private float m_hitDamage = 100f;

    [SerializeField]
    private float m_maxIntervalTime = 0.1f;

    private float m_intervalTime = 0;

    private PlayerBlownAway m_blownAwayScript;

    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_blownAwayScript = GetComponent<PlayerBlownAway>();
        m_playerEvents = GetComponent<PlayerEvents>();
    }

    private void Update()
    {
        CountIntervalTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_blownAwayScript.IsBlownAway || 0 < m_intervalTime)
        {
            return;
        }

        if (collision.collider.CompareTag("Enemy"))
        {


            if (collision.gameObject.TryGetComponent<EnemyDamageable>(out EnemyDamageable damageable))
            {
                damageable.TakeDamage(m_hitDamage);
            }

            m_playerEvents.OnBlowAwayAttack?.Invoke();

            SetAttackInterval();

        }
    }

    private void SetAttackInterval()
    {
        m_intervalTime = m_maxIntervalTime;
    }

    private void CountIntervalTime()
    {
        if (m_intervalTime <= 0)
        {
            return;
        }

        m_intervalTime -= Time.deltaTime;

        if (m_intervalTime < 0)
        {
            m_intervalTime = 0;
        }
    }
}
