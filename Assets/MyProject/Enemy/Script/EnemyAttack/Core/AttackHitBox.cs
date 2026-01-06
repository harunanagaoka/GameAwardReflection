using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private Vector3 m_basePosition;

    private float m_blownAwayPower = 1;

    private float m_blownAwayTime = 1;

    private float m_damage = 1;

    public void Initialize(Vector3 basePosition,float blownAwayPower, float blownAwayTime,float damage)
    {
        m_basePosition = basePosition;
        m_blownAwayPower = blownAwayPower;
        m_blownAwayTime = blownAwayTime;
        m_damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerBlownAway>(out PlayerBlownAway playerBlowAway))
        {
            playerBlowAway.BlowAway(m_basePosition, m_blownAwayPower, m_blownAwayTime);
        }

        if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
        {
            playerDamageable.TakeDamage(m_damage);
        }
    }
}
