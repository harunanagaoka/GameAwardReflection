using UnityEngine;

public class PlayerComboAura : MonoBehaviour
{
    ComboManager m_comboManager;

    [SerializeField]
    GameObject[] m_auraObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_comboManager = GetComponent<ComboManager>();
    }


    void Update()
    {
        SetAura();
    }

    private void SetAura()
    {
        int currentCombo = m_comboManager.CurrentCombo;

        // 今回ONにしたいオーラのindex
        // -1 = 全部OFF
        int targetIndex = -1;

        if (currentCombo >= 1 && currentCombo <= m_auraObjects.Length)
        {
            targetIndex = currentCombo - 1;
        }

        for (int i = 0; i < m_auraObjects.Length; i++)
        {
            bool shouldBeActive = (i == targetIndex);

            // 今の状態と違うときだけ変更する
            if (m_auraObjects[i].activeSelf != shouldBeActive)
            {
                m_auraObjects[i].SetActive(shouldBeActive);
            }
        }

    }
}
