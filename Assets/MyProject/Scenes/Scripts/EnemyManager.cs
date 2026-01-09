using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private EnemySpawner m_enemySpawner;

    private EnemyEvents m_bossEvents;

    public event Action OnBossEnemyDied;

    private void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnBossDefeated()
    {
        OnBossEnemyDied?.Invoke();
    }

    public EnemyDamageable SpawnBossEnemy(EnemyData enemy)
    {
        GameObject bossObject = m_enemySpawner.SpawnEnemy(enemy);

        m_bossEvents = bossObject.GetComponent<EnemyEvents>();
        m_bossEvents.OnDeath.AddListener(OnBossDefeated);

        return bossObject.GetComponent<EnemyDamageable>();
    }
}
