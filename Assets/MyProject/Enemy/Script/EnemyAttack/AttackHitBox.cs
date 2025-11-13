using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private AttackData m_attackData;

    public AttackData Data { get { return m_attackData; } }

    private void OnValidate()
    {
        // AttackData アタッチし忘れ防止　エディター上で警告される
        if (m_attackData == null)
        {
            Debug.LogWarning($"AttackData が設定されていません: {gameObject.name}", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_attackData == null)
        {
            Debug.LogWarning($"AttackData が設定されていません: {gameObject.name}", this);
            return;
        }

        if (other.TryGetComponent<PlayerBlownAway>(out PlayerBlownAway playerBlowAway))
        {
            if(transform.parent.position == null)
            {
                Debug.Log("EnemyAttackのparentが設定されていません");
                return;
            }

            playerBlowAway.BlowAway(transform.parent.position, m_attackData.BlownAwayPower, m_attackData.BlownAwayTime);
        }

        if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
        {
            playerDamageable.TakeDamage(m_attackData.Damage);
        }
    }
}
