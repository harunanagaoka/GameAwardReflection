using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Slider))]
public class BossHPBarUI : MonoBehaviour
{
    private EnemyEvents m_enemyEvents;

    [SerializeField]
    private EnemyManager m_enemyManager;

    [Header("即時減少のHPバー")]
    [SerializeField]
    private List<Image> HPBars = new List<Image>();

    [Header("フェーズごとの遅延HPバー")]
    [SerializeField]
    private List<Image> DelayBars = new List<Image>();

    [Header("待機HPバー")]
    [SerializeField]
    private List<Image> HPBank = new List<Image>();

    [Header("フェーズごとの攻撃ヒット数")]
    [SerializeField]
    private List<int> HitsAttack = new List<int>();

    private EnemyDamageable m_bossHP;

    private bool m_isInitialized = false;

    // 現在のフェーズ
    private int currentPhase = 0;
    // 現在のフェーズでのヒット数
    private int currentHitCount = 0;

    private void Start()
    {
        Image hpBarImage = HPBars[0];
        hpBarImage.fillAmount = 1f;

        m_enemyManager.OnBossJoined += Initialize;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= Initialize;

        if (m_enemyEvents != null)
            m_enemyEvents.OnDamage.RemoveListener(ReduceHPUI);
    }

    void Update()
    {
        if (!m_isInitialized)
        {
            return;
        }

        // debug用
        if (Input.GetKeyDown(KeyCode.Space)) { ReduceHPUI(); }
    }

    private void Initialize()
    {
        // ★ すでに初期化済みなら何もしない（AddListener の多重登録を防ぐ）
        if (m_isInitialized)
            return;

        m_bossHP = m_enemyManager.BossHP;

        // ★ EnemyEvents を取得
        m_enemyEvents = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();

        // ★ OnDamage に ReduceHPUI を登録
        m_enemyEvents.OnDamage.AddListener(ReduceHPUI);

        m_isInitialized = true;
    }

    public void ReduceHPUI()
    {
        if (!m_isInitialized || currentPhase >= HitsAttack.Count)
            return;

        currentHitCount++;

        int totalHits = HitsAttack[currentPhase];
        float hpRate = Mathf.Clamp01(1f - (float)currentHitCount / totalHits);

        // 即時バー更新
        HPBars[currentPhase].fillAmount = hpRate;

        // 遅延バー開始
        StartCoroutine(
            DelayAndSmoothDecreaseBar(DelayBars[currentPhase].fillAmount, hpRate)
        );

        // フェーズ切り替え
        if (currentHitCount >= totalHits)
        {
            SwitchPhase();
        }
    }

    private void SwitchPhase()
    {
        currentPhase++;
        currentHitCount = 0;

        if(DelayBars[currentPhase-1].fillAmount == 0f)
        {
            DelayBars[currentPhase - 1].fillAmount = 0f;
        }

        if (currentPhase >= HPBars.Count)
        {
            return;
        }

        HPBank[currentPhase - 1].gameObject.SetActive(false);
        HPBars[currentPhase - 1].fillAmount = 0f;

        HPBars[currentPhase].fillAmount = 1f;
        DelayBars[currentPhase].fillAmount = 1f;
    }

    IEnumerator DelayAndSmoothDecreaseBar(float startValue, float targetValue)
    {
        var delayBar = DelayBars[currentPhase];

        yield return new WaitForSeconds(0.2f);

        while (delayBar.fillAmount > targetValue)
        {
            delayBar.fillAmount =
                Mathf.MoveTowards(delayBar.fillAmount, targetValue, 0.5f * Time.deltaTime);

            yield return null;
        }
    }
}
