using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private AttackData m_attackData;

    public AttackData Data { get { return m_attackData; } }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<PlayerBlownAway>(out PlayerBlownAway playerBlowAway))
        {
            playerBlowAway.BlowAway(m_attackData.BlownAwayDirection, m_attackData.BlownAwayPower, m_attackData.BlownAwayTime);
        }

        if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
        {
            playerDamageable.TakeDamage(m_attackData.Damage);
        }
    }
}
