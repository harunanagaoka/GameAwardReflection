using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUIRealTime : MonoBehaviour
{
    private EnemyEvents m_enemyEvents;

    [SerializeField]
    private EnemyManager m_enemyManager;

    [SerializeField]
    private PhaseController m_phaseController;

    [Header("即時減少のHPバー")]
    [SerializeField]
    private List<Image> HPBars = new List<Image>();

    [Header("フェーズごとの遅延HPバー")]
    [SerializeField]
    private List<Image> DelayBars = new List<Image>();

    [Header("待機HPバー")]
    [SerializeField]
    private List<Image> HPBank = new List<Image>();

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
        m_phaseController.OnPhaseChanged += SwitchPhase;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= Initialize;
        m_phaseController.OnPhaseChanged -= SwitchPhase;

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
        //if (Input.GetKeyDown(KeyCode.Space)) { ReduceHPUI(); }
    }

    private void Initialize()
    {
        // ★ すでに初期化済みなら何もしない（AddListener の多重登録を防ぐ）
        if (m_isInitialized)
            return;

        // ★ EnemyEvents を取得
        m_enemyEvents = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();

        // ★ OnDamage に ReduceHPUI を登録
        m_enemyEvents.OnDamage.AddListener(ReduceHPUI);

        m_isInitialized = true;
    }

    public void ReduceHPUI()
    {
        if (!m_isInitialized)
            return;

        float hpRate = m_phaseController.GetCurrentPhaseProgress();

        // 即時バー更新
        HPBars[currentPhase].fillAmount = hpRate;

       

        // 遅延バー開始
        StartCoroutine(
            DelayAndSmoothDecreaseBar(DelayBars[currentPhase].fillAmount, hpRate)
        );
    }

    private void SwitchPhase(int pastPhase,int nextPhase)
    {
        currentPhase++;
        currentHitCount = 0;

        if (DelayBars[currentPhase - 1].fillAmount == 0f)
        {
            DelayBars[currentPhase - 1].fillAmount = 0f;
        }

        if (currentPhase >= HPBars.Count)
        {
            return;
        }

        HPBank[currentPhase - 1].gameObject.SetActive(false);
        HPBars[currentPhase - 1].fillAmount = 0f;

        float HPRate = m_phaseController.GetCurrentPhaseProgress();

        HPBars[currentPhase].fillAmount = HPRate;
        DelayBars[currentPhase].fillAmount = HPRate;
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
