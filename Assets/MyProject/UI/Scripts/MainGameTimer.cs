using UnityEngine;

public class MainGameTimer : MonoBehaviour
{
    [SerializeField]
    private float m_maxTime = 0;

    [SerializeField]
    private bool m_startOnAwake = false;

    private float m_currentTime = 0;

    private bool m_isTimerRunning = false;

    public float MaxTime => m_maxTime;

    public float CurrentTime => m_currentTime;

    private void Awake()
    {
        if (m_startOnAwake)
        {
            ResetTimer();
            StartTimer();
        }
    }

    void Update()
    {
        if (m_currentTime <= 0)
        {
            m_currentTime = 0;
            m_isTimerRunning = false;
        }

        if (m_isTimerRunning)
        {
            m_currentTime -= Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        m_currentTime = m_maxTime;
    }

    public void StartTimer()
    {
        m_currentTime = m_maxTime;
        m_isTimerRunning = true;
    }

    public void StopTimer()
    {
        m_isTimerRunning = false;
    }

    public void DecreseTime(float time)
    {
        m_currentTime -= time;
    }
}




