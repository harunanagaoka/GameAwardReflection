using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using System.Collections.Generic;

public class BossHpBarUI : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    [SerializeField]
    private Image m_hpFillImage; // HPバーのImage（TypeはFilledに設定）

    [SerializeField]
    private Image m_decreaseHPFillImage; // 減少するHPバーのImage（TypeはFilledに設定）

    [SerializeField]
    private List<float> faseRatioCurrent = new List<float>();  //hpのフェーズ比率リスト

    [SerializeField]
    private List<Image> Fillnum = new List<Image>();    //フェーズ数分のイメージリスト

    private EnemyDamageable m_bossHP;

    private bool m_isInitialized = false;
    private float m_prevHpRate = 1f;
    private Coroutine m_decreaseCoroutine;

    [SerializeField]
    private float m_decreaseDelay = 1.0f; // 遅延秒数
    [SerializeField]
    private float m_decreaseDuration = 0.5f; // アニメーション秒数

    private void Start()
    {
        m_enemyManager.OnBossJoined += Initialize;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= Initialize;
    }

    void Update()
    {
        if (!m_isInitialized)
        {
            return;
        }

        float hpRate = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;
        m_hpFillImage.fillAmount = hpRate;

        // HPが減ったときのみコルーチンを開始
        if (hpRate < m_prevHpRate)
        {
            if (m_decreaseCoroutine != null)
            {
                StopCoroutine(m_decreaseCoroutine);
            }
            m_decreaseCoroutine = StartCoroutine(DelayAndSmoothDecreaseBar(hpRate));
        }

        m_prevHpRate = hpRate;
    }

    private IEnumerator DelayAndSmoothDecreaseBar(float targetRate)
    {
        yield return new WaitForSeconds(m_decreaseDelay);

        float start = m_decreaseHPFillImage.fillAmount;
        float time = 0f;

        while (time < m_decreaseDuration)
        {
            time += Time.deltaTime;
            m_decreaseHPFillImage.fillAmount = Mathf.Lerp(start, targetRate, time / m_decreaseDuration);
            yield return null;
        }
        m_decreaseHPFillImage.fillAmount = targetRate;
    }

    private void Initialize()
    {
        m_bossHP = m_enemyManager.BossHP;
        float hpRate = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;
        m_hpFillImage.fillAmount = hpRate;
        m_decreaseHPFillImage.fillAmount = hpRate;
        m_prevHpRate = hpRate;
        m_isInitialized = true;
    }
}
