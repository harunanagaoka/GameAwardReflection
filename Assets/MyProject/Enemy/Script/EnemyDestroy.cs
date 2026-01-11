
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    private EnemyEvents m_enemyEvents;

    public void Initialize(EnemyEvents events)
    {
        m_enemyEvents = events;
        m_enemyEvents.OnDeath.AddListener(DestroyEnemy);
    }

    //void Start()
    //{
    //    m_enemyEvents = GetComponent<EnemyEvents>();
    //    m_enemyEvents.OnDeath.AddListener(DestroyEnemy);
    //}


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
