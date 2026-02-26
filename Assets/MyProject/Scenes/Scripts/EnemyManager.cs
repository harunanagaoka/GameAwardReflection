using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private EnemySpawner m_enemySpawner;

    private EnemyEvents m_bossEvents;

    private MainGameEvents m_mainGameEvents;

    private EnemyAttackController m_bossAttackController;//‚»‚Ì‚¤‚¿•¡”‘Î‰ž‚É‚·‚é

    private EnemyMoveController m_bossMoveController;

    private EnemyDamageable m_bossHP;

    private GameObject m_bossEnemy;//bossHP,bossEvents‚Ö‚ÌŽQÆ‚à‚±‚¿‚ç‚É“‡—\’è

    public event Action OnBossJoined = delegate { };

    public event Action OnBossEnemyDied = delegate { };

    public GameObject BossEnemy => m_bossEnemy;

    public EnemyDamageable BossHP => m_bossHP;

    private void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Initialize(MainGameEvents events)
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
        m_mainGameEvents = events;
        m_mainGameEvents.OnPhaseTransitionStart.AddListener(OnPhaseTransitionStart);
        //m_mainGameEvents.OnPhaseTransitionEnd.AddListener(OnPhaseTransitionEnd);


    }

    private void OnBossDefeated()
    {
        OnBossEnemyDied?.Invoke();
    }

    public EnemyDamageable SpawnBossEnemy(EnemyData enemyData,int currentphase)
    {
        GameObject bossObject = m_enemySpawner.SpawnEnemy(enemyData);
        m_bossEnemy = bossObject;
        m_bossEvents = bossObject.AddComponent<EnemyEvents>();
        m_bossHP = bossObject.AddComponent<EnemyDamageable>();
        var attackFactory = bossObject.AddComponent<EnemyAttackFactory>();
        var enemyMover = bossObject.AddComponent<EnemyMover>();
        m_bossAttackController = bossObject.AddComponent<EnemyAttackController>();
        m_bossMoveController = bossObject.AddComponent<EnemyMoveController>();
        var destroyer = bossObject.AddComponent<EnemyDestroy>();
        var effectPlayer = bossObject.GetComponent<EnemyEffectPlayer>();
        var SEPlayer = bossObject.GetComponent<EnemySE>();

        m_bossEvents.OnDeath.AddListener(OnBossDefeated);
        m_bossHP.Initialize(enemyData, m_bossEvents);
        m_bossAttackController.Initialize(enemyData, m_bossEvents,attackFactory);
        m_bossMoveController.Initialize(enemyData, enemyMover);
      //  SetEnemyPhase(currentphase);
        destroyer.Initialize(m_bossEvents);
        effectPlayer.Initialize(m_bossEvents);
        SEPlayer.Initialize(m_bossEvents);

        OnBossJoined?.Invoke();

        return m_bossHP;
    }

    public void OnPhaseTransitionStart()
    {
        m_bossAttackController.StopAllTasks();
        m_bossMoveController.StopAllTasks();
    }

    public void SetEnemyPhase(int phase)
    {
        m_bossAttackController.OnPhaseChanged(phase);
        m_bossMoveController.OnPhaseChanged(phase);
    }
}
