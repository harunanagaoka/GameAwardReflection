using System.Collections;
using UnityEngine;


public class EnemyDamageable : Damageable
{
    private EnemyData m_enemyData;

    private EnemyEvents m_enemyEvents;

    private Material m_material;

    private Color m_defaultColor;

    private float m_maxHitPoint;

    private float m_currentHitPoint;

    private bool m_isDead = false;

    private bool m_wasInitialized = false;

    public float HitPoint => m_currentHitPoint;

    public float MaxHitPoint => m_enemyData.HP;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Initialize(EnemyData data,EnemyEvents events)
    {
        m_enemyData = data;
        m_maxHitPoint = data.HP;
        m_currentHitPoint = data.HP;
        m_maxHitInterval = data.DamageInterval;
        m_enemyEvents = events;
        m_enemyEvents.OnDamage.AddListener(ShowDamageEffect);
        m_enemyEvents.OnDamagePenalty.AddListener(DecreaseHP);

        m_wasInitialized = true;

        // m_material = GetComponent<Renderer>().material;
        // m_defaultColor = m_material.color;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (!m_wasInitialized) {
            return;
        }

        base.Update();

        if (Input.GetKeyDown(KeyCode.O)) {
            //デバッグよう
            Debug.Log("デバッグ用、50ダメージ");
            base.TakeDamage(50);
        }

        if (!m_isDead && m_currentHitPoint < 0)
        {
            m_isDead = true;
            OnDeathEvent();
        }
    }

    protected override void OnDamageEvent()
    {
        m_enemyEvents.OnDamage?.Invoke();
    }

    protected override void OnDamagePenaltyEvent(float damage)
    {
        m_enemyEvents.OnDamagePenalty?.Invoke(damage);
    }

    private void OnDeathEvent()
    {
        m_enemyEvents.OnDeath?.Invoke();
    }

    private void DecreaseHP(float damage)
    {
        m_currentHitPoint -= damage;
    }

    private void ShowDamageEffect()
    {
        StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        //m_material.color = m_enemyData.OnDamageColor;

        yield return new WaitForSeconds(m_enemyData.DamageEffectTime);

        //m_material.color = m_defaultColor;

        yield return null;
    }
}
