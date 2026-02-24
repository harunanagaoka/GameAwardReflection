using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField]
    float m_damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyDamageable>(out EnemyDamageable enemy))
        {
            enemy.TakeDamage(m_damage);
        }
    }

    public void SetDamage(float damage)
    {
        m_damage = damage;
    }
}
