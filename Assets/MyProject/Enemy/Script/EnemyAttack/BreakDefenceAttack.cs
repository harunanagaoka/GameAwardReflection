using UnityEngine;

public class BreakDefenceAttack : MonoBehaviour
{
    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_playerEvents = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
    }

    public void BreakDefence()
    {
        m_playerEvents.OnDefenceEnd.Invoke();
    }
}
