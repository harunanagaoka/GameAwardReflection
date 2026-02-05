using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BossHpBarUI : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    [SerializeField]
    private List<Image> m_hpFillImages = new List<Image>();

    [SerializeField]
    private List<Image> m_decreaseHPFillImages = new List<Image>();

    [SerializeField]
    private List<float> faseRatioCurrent = new List<float>();
    // HPフェーズ比率

    private EnemyDamageable m_bossHP;

    private bool m_isInitialized = false;
    // 初期化が完了したかどうか

    private float m_prevHpRate = 1f;
    // 前フレームのHP割合（HP減少を検知するため）

    private Coroutine m_decreaseCoroutine;
    // 遅延減少バーのアニメーション用コルーチン

    [SerializeField]
    private float m_decreaseDelay = 1.0f;
    // HPが減ってから遅延バーが動き始めるまでの時間

    [SerializeField]
    private float m_decreaseDuration = 0.5f;
    // 遅延バーが滑らかに減るアニメーション時間

    private int m_currentPhase = 0; // 現在のフェーズ番号

    private void Start()
    {
        // ボスが登場したときに Initialize を呼ぶよう登録
        m_enemyManager.OnBossJoined += Initialize;
    }

    private void OnDisable()
    {
        // 無効化時にイベント購読解除（メモリリーク防止）
        m_enemyManager.OnBossJoined -= Initialize;
    }

    void Update()
    {
        if (!m_isInitialized)
        {
            return;
        }

        // 現在のHP割合
        float hpRate = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;

        // 即時反映バー更新
        m_hpFillImage.fillAmount = hpRate;

        // HPが減ったときだけ遅延バーを動かす
        if (hpRate < m_prevHpRate)
        {
            if (m_decreaseCoroutine != null)
            {
                StopCoroutine(m_decreaseCoroutine);
            }

            m_decreaseCoroutine = StartCoroutine(DelayAndSmoothDecreaseBar(hpRate));
        }

        // ★ HPが0になった瞬間だけフェーズ切り替え
        if (hpRate <= 0f && m_prevHpRate > 0f)
        {
            SwitchPhase();
        }

        // 次フレーム用に保存
        m_prevHpRate = hpRate;
    }


    private IEnumerator DelayAndSmoothDecreaseBar(float targetRate)
    {
        // 指定秒数だけ待つ（ダメージ演出のため）
        yield return new WaitForSeconds(m_decreaseDelay);

        float start = m_decreaseHPFillImage.fillAmount;
        // アニメーション開始時点の fillAmount

        float time = 0f;

        // 指定時間かけて滑らかに減らす
        while (time < m_decreaseDuration)
        {
            time += Time.deltaTime;

            // Lerpで徐々に targetRate に近づける
            m_decreaseHPFillImage.fillAmount =
                Mathf.Lerp(start, targetRate, time / m_decreaseDuration);

            yield return null;
        }

        // 最終的に目標値に合わせる
        m_decreaseHPFillImage.fillAmount = targetRate;
    }

    private void Initialize()
    {
        // ボスのHP情報を取得
        m_bossHP = m_enemyManager.BossHP;

        // 初期HP割合を計算
        float hpRate = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;

        // 即時バーと遅延バーを満タンに設定
        m_hpFillImage.fillAmount = hpRate;
        m_decreaseHPFillImage.fillAmount = hpRate;

        // 前回HP割合も初期値に
        m_prevHpRate = hpRate;

        // 初期化完了
        m_isInitialized = true;
    }

    private void SwitchPhase()
    {
        // 現在のフェーズのバーを非表示
        if (Fillnum != null && m_currentPhase < Fillnum.Count)
        {
            Fillnum[m_currentPhase].gameObject.SetActive(false);
        }

        // 次のフェーズへ
        m_currentPhase++;

        // 次のフェーズのバーを表示
        if (Fillnum != null && m_currentPhase < Fillnum.Count)
        {
            Fillnum[m_currentPhase].gameObject.SetActive(true);

            // HPバーを満タンにリセット
            m_hpFillImage = Fillnum[m_currentPhase];
            m_hpFillImage.fillAmount = 1f;
            m_decreaseHPFillImage = Fillnum[m_currentPhase];
            m_decreaseHPFillImage.fillAmount = 1f;
        }

        // 必要に応じて他の初期化処理や演出を追加
    }
}

