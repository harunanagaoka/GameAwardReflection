
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{

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

    //　敵オブジェクト
    [SerializeField]
    private GameObject m_enemy;

    //　敵のイベントスクリプト
    private EnemyEvents m_enemyEvent;

    private PlayerEvents m_playerEvent;

    private void Start()
    {
        m_enemyEvent = m_enemy.GetComponent<EnemyEvents>();

        m_playerEvent = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();

        //イベントへの処理の登録方法
        m_enemyEvent.OnDamage.AddListener(SlowDown);
        m_playerEvent.OnDamage.AddListener(SlowDown);
        m_playerEvent.OnBlownAway.AddListener(SlowDown);

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



}

