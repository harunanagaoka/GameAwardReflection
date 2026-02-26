
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    private EnemyEvents m_enemyEvents;

    public void Initialize(EnemyEvents events)
    {
        m_enemyEvents = events;
       // m_enemyEvents.OnDeath.AddListener(DestroyEnemy);
    }

    


    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
