using UnityEngine;

public class PlayerBlowAwayAttack : MonoBehaviour
{
    private PlayerBlownAway m_blownAwayScript;

    [SerializeField]
    private float m_damage = 100;

    private void Start()
    {
        m_blownAwayScript = GetComponent<PlayerBlownAway>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_blownAwayScript.IsBlownAway)
        {
            return;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<EnemyDamageable>(out EnemyDamageable damageable))
            {
                damageable.TakeDamage(m_damage);
            }
        }
    }
}
