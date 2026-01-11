using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private EnemySpawner m_enemySpawner;

    private EnemyEvents m_bossEvents;

    private EnemyAttackController m_bossAttackController;//ÇªÇÃÇ§Çøï°êîëŒâûÇ…Ç∑ÇÈ

    public event Action OnBossEnemyDied;

    private void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnBossDefeated()
    {
        OnBossEnemyDied?.Invoke();
    }

    public EnemyDamageable SpawnBossEnemy(EnemyData enemyData,int currentphase)
    {
        GameObject bossObject = m_enemySpawner.SpawnEnemy(enemyData);
        m_bossEvents = bossObject.AddComponent<EnemyEvents>();
        var damageable = bossObject.AddComponent<EnemyDamageable>();
        var attackFactory = bossObject.AddComponent<EnemyAttackFactory>();
        m_bossAttackController = bossObject.AddComponent<EnemyAttackController>();
        var destroyer = bossObject.AddComponent<EnemyDestroy>();
        var effectPlayer = bossObject.GetComponent<EnemyEffectPlayer>();
        var SEPlayer = bossObject.GetComponent<EnemySE>();

        m_bossEvents.OnDeath.AddListener(OnBossDefeated);
        damageable.Initialize(enemyData, m_bossEvents);
        m_bossAttackController.Initialize(enemyData, m_bossEvents,attackFactory);
        SetEnemyPhase(currentphase);
        destroyer.Initialize(m_bossEvents);
        effectPlayer.Initialize(m_bossEvents);
        SEPlayer.Initialize(m_bossEvents);

        return damageable;
    }

    public void SetEnemyPhase(int phase)
    {
        m_bossAttackController.OnPhaseChanged(phase);
    }
}
