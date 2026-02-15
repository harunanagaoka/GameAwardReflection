using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private Vector3 m_basePosition;

    private float m_blownAwayPower = 1;

    private float m_blownAwayTime = 1;

    private float m_damage = 1;

    private BreakDefenceAttack m_breakDefence = null;

    public void Initialize(Vector3 basePosition,float blownAwayPower, float blownAwayTime,float damage)
    {
        m_basePosition = basePosition;
        m_blownAwayPower = blownAwayPower;
        m_blownAwayTime = blownAwayTime;
        m_damage = damage;

        if (TryGetComponent<BreakDefenceAttack>(out BreakDefenceAttack breakDefence))
        {
            m_breakDefence = breakDefence;
        }
    }

    private void Update()
    {
        //m_basePosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (m_breakDefence)
        {
            //–hŒäƒLƒƒƒ“ƒZƒ‹
            m_breakDefence.BreakDefence();
        }

        if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
        {
            playerDamageable.TakeDamage(m_damage);
        }

        if (other.TryGetComponent<PlayerBlownAway>(out PlayerBlownAway playerBlowAway))
        {
            playerBlowAway.BlowAway(transform.position, m_blownAwayPower, m_blownAwayTime);
        }


    }
}
