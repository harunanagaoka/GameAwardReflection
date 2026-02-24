using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_comboText;
    [SerializeField] private GameObject m_root;

    private PlayerBlowAwayAttack m_blownAwayAttack;

    private PlayerEvents m_playerEvents;

    void Start()
    {
        GameObject player = PlayerManager.Instance.Players[0];
        m_blownAwayAttack = player.GetComponent<PlayerBlowAwayAttack>();
        m_playerEvents = player.GetComponent<PlayerEvents>();
        m_playerEvents.OnComboStateChanged.AddListener(SetVisible);

        if (m_root == null)
        {
            m_root = gameObject;
        }

        SetVisible(false);
    }


    void Update()
    {
        if (!m_root.activeSelf)
        {
            return;
        }

       // m_comboText.text = m_blownAwayAttack.CurrentCombo.ToString();
    }

    private void SetVisible(bool isVisible)
    {
        m_root.SetActive(isVisible);
        m_comboText.text = m_blownAwayAttack.CurrentCombo.ToString();
    }
}
