using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyDamageable : Damageable
{
    [SerializeField]
    private EnemyData m_enemyData;

    private EnemyEvents m_enemyEvents;

    private Material m_material;

    private Color m_defaultColor;

    protected override void Start()
    {
        base.Start();

        m_enemyEvents = GetComponent<EnemyEvents>();
        m_enemyEvents.OnDamage.AddListener(ShowDamageEffect);

        m_material = GetComponent<Renderer>().material;
        m_defaultColor = m_material.color;
        m_hitPoint = m_enemyData.HP;
    }

    protected override void OnDamageEvent()
    {
        m_enemyEvents.OnDamage?.Invoke();
    }

    protected override void OnDeathEvent()
    {
        m_enemyEvents.OnDeath?.Invoke();
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
