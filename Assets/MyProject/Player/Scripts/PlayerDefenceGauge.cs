using UnityEngine;

public class PlayerDefenceGauge : MonoBehaviour
{
    [SerializeField]
    private float m_maxGaugeValue = 100;

    [SerializeField]
    private float m_decreceValue = 1;

    [SerializeField]
    private float m_recoveryValue = 1;

    [SerializeField]
    private float m_attackDecreceValue = 100;

    private float m_currentGaugeValue;

    private PlayerEvents m_playerEvents;

    private PlayerBlownAway m_playerBlownAway;

    private bool m_isKeepDefence;

    public float DefencePercentage => m_currentGaugeValue / m_maxGaugeValue;


    void Start()
    {
        m_currentGaugeValue = m_maxGaugeValue;
        m_playerBlownAway = GetComponent<PlayerBlownAway>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isKeepDefence = true);
        //m_playerEvents.OnDefenceBleaked.AddListener(() => m_isKeepDefence = false);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isKeepDefence = false);
        m_playerEvents.OnBlownAway.AddListener(() => AttackDecreceDefenceGauge());
    }

    void Update()
    {

        if (m_isKeepDefence && !m_playerBlownAway.IsBlownAway)
        {
           // DecreceDefenceGauge(m_decreceValue);

            if(m_currentGaugeValue <= 0)
            {
                ProcessGuardGaugeBreak();
            }
        }

        if(!m_isKeepDefence && !m_playerBlownAway.IsBlownAway)
        {
            RecoveryDefenceGauge(m_recoveryValue);
        }
    }

    public void DecreceDefenceGauge(float value)
    {
        if (m_currentGaugeValue > 0)
        {
            m_currentGaugeValue -= value;
        }
    }

    private void RecoveryDefenceGauge(float value)
    {
        if(m_currentGaugeValue < m_maxGaugeValue)
        {
            m_currentGaugeValue += value;
        }
    }

    private void ProcessGuardGaugeBreak()
    {
        m_currentGaugeValue = 0;
        m_playerEvents.OnDefenceBleaked?.Invoke();
    }

    public void AttackDecreceDefenceGauge()
    {
        if (m_isKeepDefence)
        {
            m_currentGaugeValue -= m_attackDecreceValue;
        }
    }
}
