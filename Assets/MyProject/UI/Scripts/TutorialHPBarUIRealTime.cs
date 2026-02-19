using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHPBarUIRealTime : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    [SerializeField]
    private PhaseController m_phaseController;

    private EnemyEvents m_enemyEvents;

    [Header("即時減少のHPバー")]
    [SerializeField]
    private Image m_HPBar = null;

    [Header("フェーズごとの遅延HPバー")]
    [SerializeField]
    private Image m_delayBar = null;

    private bool m_isInitialized = false;


    private void Start()
    {
        Image hpBarImage = m_HPBar;
        hpBarImage.fillAmount = 1f;

        m_enemyManager.OnBossJoined += Initialize;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= Initialize;

        if (m_enemyEvents != null)
            m_enemyEvents.OnDamage.RemoveListener(ReduceHPUI);
    }

    private void Initialize()
    {
        // ★ すでに初期化済みなら何もしない（AddListener の多重登録を防ぐ）
        if (m_isInitialized)
            return;

        // ★ Events を取得
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
        m_HPBar.fillAmount = hpRate;


        // 遅延バー開始
        StartCoroutine(
            DelayAndSmoothDecreaseBar(m_HPBar.fillAmount, hpRate)
        );
    }

    IEnumerator DelayAndSmoothDecreaseBar(float startValue, float targetValue)
    {
        yield return new WaitForSeconds(0.2f);

        while (m_delayBar.fillAmount > targetValue)
        {
            m_delayBar.fillAmount =
                Mathf.MoveTowards(m_delayBar.fillAmount, targetValue, 0.5f * Time.deltaTime);

            yield return null;
        }
    }
}
