using UnityEngine;


public class PlayerState : MonoBehaviour
{
    private PlayerEvents m_playerEvents;

    public enum PlState
    {
        Idle,
        BlownAway,
        Inertia,
        Defence,
        Stun
    }

    private PlState m_currentState = PlState.Idle;

    public PlState CurrentState => m_currentState;

    void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
    }

    public void SetState(PlState state)
    {
        m_currentState = state;
    }
}
