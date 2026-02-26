
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    //　Time.timeScaleに設定する値
    [SerializeField]
    private float AttckHitTimeScale = 0.1f;

    [SerializeField]
    private float DefenceHitTimeScale = 0.1f;


    //　時間を遅くしている時間
    [SerializeField]
    private float AttckSlowTime = 1f;

    [SerializeField]
    private float DefenceSlowTime = 1f;

    //　経過時間
    private float elapsedTime = 0f;

    //　時間を遅くしているかどうか
    private bool isSlowDown = false;

    private bool isPlayerSlowDown = false;

    //　敵のイベントスクリプト
    private EnemyEvents m_enemyEvent;

    private PlayerEvents m_playerEvent;

    [SerializeField]
    OnClickAttack onClickAttack;


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
            if (elapsedTime >= AttckSlowTime)
            {
                SetNormalTime();
            }
        }
        else if (isPlayerSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= DefenceSlowTime)
            {
                SetNormalTime();
            }
        }
    }
    //　時間を遅らせる処理
    public void SlowDown()
    {
        if (onClickAttack)
        {
            elapsedTime = 0f;
            Time.timeScale = AttckHitTimeScale;
            isSlowDown = true;
        }
    }

    public void PlayerSlowDown()
    {
        elapsedTime = 0f;
        Time.timeScale = DefenceHitTimeScale;
        isPlayerSlowDown = true;
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
        onClickAttack = GetComponent<OnClickAttack>();
        m_enemyEvent.OnDamage.AddListener(SlowDown);
    }

    private void ResisterPlayerEvent()
    {
        //m_playerEvent = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
        //m_playerEvent.OnDefenceEnd.AddListener(PlayerSlowDown);
        //m_playerEvent.OnDamage.AddListener(SlowDown);
    }
}

