using System.Collections;
using UnityEngine;

//エフェクトの処理分けたいなあ
[RequireComponent(typeof(EnemyEvents))]
public class EnemyDamageable : Damageable
{
    [SerializeField]
    private EnemyData m_enemyData;

    private EnemyEvents m_enemyEvents;

    private Material m_material;

    private Color m_defaultColor;

    private float m_hitPoint;

    private bool m_isDead = false;

    public float HitPoint => m_hitPoint;

    protected override void Start()
    {
        base.Start();

        m_enemyEvents = GetComponent<EnemyEvents>();
        m_enemyEvents.OnDamage.AddListener(ShowDamageEffect);
        m_enemyEvents.OnDamagePenalty.AddListener(DecreaseHP);

        m_material = GetComponent<Renderer>().material;
        m_defaultColor = m_material.color;
        m_hitPoint = m_enemyData.HP;
    }

    private void Update()
    {
        base.Update();

        if (!m_isDead && m_hitPoint < 0)
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
        m_hitPoint -= damage;
    }

    private void ShowDamageEffect()
    {
        StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        m_material.color = m_enemyData.OnDamageColor;

        yield return new WaitForSeconds(m_enemyData.DamageEffectTime);

        m_material.color = m_defaultColor;

        yield return null;
    }
}
