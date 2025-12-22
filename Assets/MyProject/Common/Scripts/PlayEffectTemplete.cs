using UnityEngine;

public class PlayEffectTemplete : MonoBehaviour
{
    //エフェクト再生用のテンプレートです。
    //必要な部分をコピペして使ってください
    //enemyに使う場合はm_playerEventsを消去してください。
    //playerに使う場合はm_enemyEventsを消去してください。

    //以下どちらか選ぶ
    [SerializeField]
    private EnemyEvents m_enemyEvents;

    [SerializeField]
    private PlayerEvents m_playerEvents;


    //PlayerEventsのOnBlowAwayを再生する場合
    [SerializeField]
    private GameObject m_blownAwayEffect;

    private GameObject m_playingEffect;

    void Start()
    {
        //イベントの登録例
        m_playerEvents.OnBlownAway.AddListener(PlayBlownAwayEffect);
        m_playerEvents.OnBlownAwayCanceled.AddListener(StopBlownAwayEffect);
        m_playerEvents.OnBlownAwayEnd.AddListener(StopBlownAwayEffect);
    }


    //以下Playerのエフェクト再生用の関数例

    //エフェクト再生用
    private void PlayBlownAwayEffect()
    {
        m_playingEffect = Instantiate(m_blownAwayEffect, transform);
    }

    //エフェクト停止用
    private void StopBlownAwayEffect()
    {
        Destroy(m_playingEffect);
    }
}
