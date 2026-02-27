using UnityEngine;

public class Set : MonoBehaviour
{
    [SerializeField]
    private ZoomShakeCamera m_zoomShakeCamera;
    [SerializeField]
    private TimeManager m_timeManager;


    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_playerEvents = PlayerManager.Instance.Players[0].GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(DeactivateEffects);
        m_playerEvents.OnDefenceEnd.AddListener(ActivateEffects);
    }

     public void ActivateEffects()
    {
        m_zoomShakeCamera.enabled = true; // ZoomShakeCameraをONにする
        m_timeManager.enabled = true; // TimeManagerをONにする
    }
    public void DeactivateEffects()
    {
        m_zoomShakeCamera.enabled = false; // ZoomShakeCameraをOFFにする
        m_timeManager.enabled = false; // TimeManagerをOFFにする
    }
}

