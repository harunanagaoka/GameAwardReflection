using UnityEngine;

public class TimeLimitBarUI : MonoBehaviour
{
    private float m_maxTime = 0;
    private float m_currentTime = 0;

    [SerializeField]
    private MainGameTimer m_timer;

    [SerializeField]
    private UnityEngine.UI.Slider m_timeLimitSlider;

    private bool m_isInitialized = false;

    void Update()
    {
        if (!m_isInitialized)
        {
            //タイミングがm_timerのStartと被り、Timeの最大値が0になる可能性があるため、Updateで初期化する
            Initialize();
        }

        //OnDamageイベントを使うべきだが、今回は簡易的にUpdateで対応する。
        m_currentTime = m_timer.CurrentTime;
        m_timeLimitSlider.value = (float)m_currentTime / m_maxTime;
    }

    private void Initialize()
    {
        m_maxTime = m_timer.CurrentTime;
        m_currentTime = m_maxTime;

        m_timeLimitSlider.value = m_currentTime / m_maxTime;//最大値は1
        m_isInitialized = true;
    }
}
