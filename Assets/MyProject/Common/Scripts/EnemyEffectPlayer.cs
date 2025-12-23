using UnityEngine;

public class EnemyEffectPlayer : MonoBehaviour
{
    //エフェクトを再生するEnemyを登録する
    [SerializeField]
    private EnemyEvents m_enemyEvents;

    //EnemyEventsのOnDamageのエフェクト
    //今回は、エフェクトの種類が増えるごとにエフェクトのプレハブを登録し、再生用の関数を増設することにします。
    [SerializeField]
    private GameObject m_damageEffect;


    void Start()
    {
        //イベントの登録例
        m_enemyEvents.OnDamage.AddListener(PlayDamageEffect);
    }

    //以下Enemyのエフェクト再生用の関数例
    private void PlayDamageEffect()
    {
       Instantiate(m_damageEffect, transform);
    }
}
