using UnityEngine;

public class BreakDefenceAttack : MonoBehaviour
{
    private PlayerEvents m_playerEvents;
    private PlayerBlownAway m_playerBlownAway;

    private void Start()
    {
        m_playerEvents = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
        m_playerBlownAway = PlayerManager.Instance.Players[0].GetComponent<PlayerBlownAway>();
    }

    public void BreakDefence()
    {
        if (!m_playerBlownAway.IsBlownAway)
        {
            m_playerEvents.OnDefenceEnd.Invoke();
        }
        
    }
}
