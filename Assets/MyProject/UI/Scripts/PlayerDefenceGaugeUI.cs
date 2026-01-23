using UnityEngine;

public class PlayerDefenceGaugeUI : MonoBehaviour
{

    [SerializeField]
    private PlayerDefenceGauge m_defenceGauge;

    [SerializeField]
    private UnityEngine.UI.Slider m_defenceGaugeSlider;

    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnResisterPlayer += Initialize;
        }
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnResisterPlayer -= Initialize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_defenceGaugeSlider.value = m_defenceGauge.DefencePercentage;
    }

    private void Initialize()
    {
        //m_defenceGauge = PlayerManager.Instance.Players[0].GetComponent<PlayerDefenceGauge>();
        m_defenceGaugeSlider.value = m_defenceGauge.DefencePercentage;
    }
}
