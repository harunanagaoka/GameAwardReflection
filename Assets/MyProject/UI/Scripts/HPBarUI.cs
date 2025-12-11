using UnityEngine;

public class HpBarUI : MonoBehaviour
{
    private float m_maxHp = 0;
    private float m_currentHp = 0;

    [SerializeField]
    private EnemyDamageable m_damageable;

    [SerializeField]
    private UnityEngine.UI.Slider m_hpSlider;

    private bool m_isInitialized = false;

    void Update()
    {
        if (!m_isInitialized)
        {
            //タイミングがEnemyDamageableのStartと被り、HPの最大値が0になる可能性があるため、Updateで初期化する
            Initialize();
        }

        //OnDamageイベントを使うべきだが、今回は簡易的にUpdateで対応する。
        m_currentHp = m_damageable.HitPoint;
        m_hpSlider.value = m_currentHp / m_maxHp;
    }

    private void Initialize()
    {
        m_maxHp = m_damageable.HitPoint;
        m_currentHp = m_maxHp;

        m_hpSlider.value = m_currentHp / m_maxHp;//最大値は1
        m_isInitialized = true;
    }
}