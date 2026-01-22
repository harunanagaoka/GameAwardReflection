using UnityEngine;

public class PlayerDefenceGauge : MonoBehaviour
{
    [SerializeField]
    private float m_maxGaugeValue = 100;

    [SerializeField]
    private float m_decreceValue = 1;

    [SerializeField]
    private float m_recoveryValue = 1;

    private float m_currentGaugeValue;

    private PlayerEvents m_playerEvents;

    private PlayerState m_playerState;

    private PlayerBlownAway m_playerBlownAway;

    private bool m_isKeepDefence;

    public float DefencePercentage => m_currentGaugeValue / m_maxGaugeValue;


    void Start()
    {
        m_playerBlownAway = GetComponent<PlayerBlownAway>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(()=> m_isKeepDefence = true);
        m_playerEvents.OnDefence.AddListener(SetDefenceState);
        m_playerState = GetComponent<PlayerState>();
        //m_playerEvents.OnDefenceBleaked.AddListener(() => m_isKeepDefence = false);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isKeepDefence = false);
        m_playerEvents.OnDefenceEnd.AddListener(SetDefenceEndState);
    }

    void Update()
    {
        PlayerState.PlState state = m_playerState.CurrentState;

        if (m_isKeepDefence && state != PlayerState.PlState.BlownAway)
        {
            DecreceDefenceGauge(m_decreceValue);

            if(m_currentGaugeValue <= 0)
            {
                ProcessGuardGaugeBreak();
            }
        }

        if(!m_isKeepDefence && state != PlayerState.PlState.BlownAway)
        {
            RecoveryDefenceGauge(m_recoveryValue);
        }
    }

    private void DecreceDefenceGauge(float value)
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

    private void SetDefenceState()
    {
        if(m_playerState.CurrentState != PlayerState.PlState.BlownAway || m_playerState.CurrentState != PlayerState.PlState.Defence)
        m_playerState.SetState(PlayerState.PlState.Defence);
    }

    private void SetDefenceEndState()
    {
        if(m_playerState.CurrentState == PlayerState.PlState.Defence || m_playerState.CurrentState != PlayerState.PlState.BlownAway)
        {
            m_playerState.SetState(PlayerState.PlState.Idle);
        }
    }

}
