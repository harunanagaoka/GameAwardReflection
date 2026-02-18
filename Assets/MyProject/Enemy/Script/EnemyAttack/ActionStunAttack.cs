using UnityEngine;
using System.Collections;

public class ActionStunAttack : MonoBehaviour
{
    private PlayerEvents m_playerEvents;
    private PlayerBlownAway m_playerBlownAway;

    [SerializeField]
    private float m_stunTime = 0;

    private void Start()
    {
        m_playerEvents = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
        m_playerBlownAway = PlayerManager.Instance.Players[0].GetComponent<PlayerBlownAway>();
    }
    
    public void StartStunCoroutine()
    {
        if(m_playerBlownAway.IsBlownAway)
        {
            return;
        }

        StartCoroutine(StunPlayer());
    }

    private IEnumerator StunPlayer()
    {
        m_playerEvents.OnStun.Invoke();
        yield return new WaitForSeconds(m_stunTime);
        m_playerEvents.OnStunEnd.Invoke();
    }
}
