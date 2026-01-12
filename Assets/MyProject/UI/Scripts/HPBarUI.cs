using UnityEngine;

[RequireComponent (typeof(UnityEngine.UI.Slider))]
public class HpBarUI : MonoBehaviour
{
    [SerializeField]
    private EnemyManager m_enemyManager;

    private EnemyDamageable m_bossHP;

    private UnityEngine.UI.Slider m_hpSlider;

    private bool m_isInitialized = false;

    private void Start()
    {
        m_hpSlider = GetComponent<UnityEngine.UI.Slider>();
        m_enemyManager.OnBossJoined += Initialize;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= Initialize;
    }

    void Update()
    {
        //タイミングがEnemyDamageableのStartと被り、HPの最大値が0になる可能性があるため、Updateで初期化する
        if (!m_isInitialized)
        {
            return;
        }

        //OnDamageイベントを使うべきだが、今回は簡易的にUpdateで対応する。

        m_hpSlider.value = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;
    }

    private void Initialize()
    {
        m_bossHP = m_enemyManager.BossHP;
        m_hpSlider.value = m_bossHP.HitPoint / m_bossHP.MaxHitPoint;//最大値は1
        m_isInitialized = true;
    }
}