using UnityEngine;

public class MainGameTimer : MonoBehaviour
{
    [SerializeField]
    private float m_maxTime = 0;

    private float m_currentTime = 0;

    private bool m_isTimerRunning = false;

    public float CurrentTime => m_currentTime;

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
        m_isTimerRunning = true;
    }
}
