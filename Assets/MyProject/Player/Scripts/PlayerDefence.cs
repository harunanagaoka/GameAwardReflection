using UnityEngine;

public class PlayerDefence : MonoBehaviour
{
    private PlayerEvents m_playerEvents;

    void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        //m_playerEvents.OnDefence.AddListener()
    }

    void Update()
    {

    }
}
