using UnityEngine;
using System.Collections.Generic;

public class PlayerEffectPlayer : MonoBehaviour
{
    //エフェクトを再生するEnemyを登録する
    [SerializeField]
    private PlayerEvents m_playerEvents;

    //EnemyEventsのOnDamageのエフェクト
    //今回は、エフェクトの種類が増えるごとにエフェクトのプレハブを登録し、再生用の関数を増設することにします。
    [SerializeField]
    private GameObject m_attackEffect;

    [SerializeField]
    private GameObject m_damageEffect;

    [SerializeField]
    private GameObject m_stunEffect;

    private readonly List<GameObject> m_stunSpawned = new();


    void Start()
    {
        //イベントの登録例
        m_playerEvents.OnBlownAwayCanceled.AddListener(PlayAttackEffect);
        m_playerEvents.OnDamage.AddListener(PlayDamageEffect);
        m_playerEvents.OnStun.AddListener(PlayStunEffect);
        m_playerEvents.OnStunEnd.AddListener(DestroyAllStunEffects);

    }

    //以下Enemyのエフェクト再生用の関数例
    private void PlayAttackEffect()
    {
        Instantiate(m_attackEffect,transform.position, m_attackEffect.transform.rotation, transform);
    }

    private void PlayDamageEffect()
    {
        Instantiate(m_damageEffect, transform.position, Quaternion.identity, transform);
    }

    private void PlayStunEffect()
    {
        var ef = Instantiate(m_stunEffect,transform.position, Quaternion.identity, transform);
        m_stunSpawned.Add(ef);
    }

    private void DestroyAllStunEffects()
    {
        for (int i = m_stunSpawned.Count - 1; i >= 0; i--)
        {
            var ps = m_stunSpawned[i];
            if (ps == null)
            {
                m_stunSpawned.RemoveAt(i);
                continue;
            }

            Destroy(ps.gameObject);
            m_stunSpawned.RemoveAt(i);
        }
    }
}
    
