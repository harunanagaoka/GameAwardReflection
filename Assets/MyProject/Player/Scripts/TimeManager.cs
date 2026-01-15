
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    //　Time.timeScaleに設定する値
    [SerializeField]
    private float timeScale = 0.1f;
    //　時間を遅くしている時間
    [SerializeField]
    private float slowTime = 1f;
    //　経過時間
    private float elapsedTime = 0f;
    //　時間を遅くしているかどうか
    private bool isSlowDown = false;


    //　敵のイベントスクリプト
    private EnemyEvents m_enemyEvent;

    private PlayerEvents m_playerEvent;


    private void Start()
    {
        m_enemyManager.OnBossJoined += ResisterEnemyEvent;
        PlayerManager.Instance.OnResisterPlayer += ResisterPlayerEvent;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= ResisterEnemyEvent;
        PlayerManager.Instance.OnResisterPlayer -= ResisterPlayerEvent;
    }

    void Update()
    {
        //　スローダウンフラグがtrueの時は時間計測
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= slowTime)
            {
                SetNormalTime();
            }
        }
    }
    //　時間を遅らせる処理
    public void SlowDown()
    {
        elapsedTime = 0f;
        Time.timeScale = timeScale;
        isSlowDown = true;
    }
    //　時間を元に戻す処理
    public void SetNormalTime()
    {
        Time.timeScale = 1f;
        isSlowDown = false;
    }

    private void ResisterEnemyEvent()
    {
        m_enemyEvent = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();
        m_enemyEvent.OnDamage.AddListener(SlowDown);
        
    }

    private void ResisterPlayerEvent()
    {
        m_playerEvent = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
        m_playerEvent.OnBlownAway.AddListener(SlowDown);
        m_playerEvent.OnDamage.AddListener(SlowDown);
    }
}

