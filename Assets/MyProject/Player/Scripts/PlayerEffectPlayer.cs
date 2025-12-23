using UnityEngine;

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

    void Start()
    {
        //イベントの登録例
        m_playerEvents.OnBlownAwayCanceled.AddListener(PlayAttackEffect);
        m_playerEvents.OnDamage.AddListener(PlayDamageEffect);

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


}
