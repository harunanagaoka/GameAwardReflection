
using UnityEngine;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyDestroy : MonoBehaviour
{

    private EnemyEvents m_enemyEvents;

    void Start()
    {
        m_enemyEvents = GetComponent<EnemyEvents>();
        m_enemyEvents.OnDeath.AddListener(DestroyEnemy);
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
