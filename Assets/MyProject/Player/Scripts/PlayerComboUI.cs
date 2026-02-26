using UnityEngine;

public class PlayerComboUI : MonoBehaviour
{
    ComboManager m_comboManager;

    [SerializeField]
    GameObject[] m_UIPrefabs; // 要素数5固定

    int m_prevCombo = -1;

    void Start()
    {
        m_comboManager = GetComponent<ComboManager>();
    }

    void Update()
    {
        TryInstantiateUI();
    }

    private void TryInstantiateUI()
    {
        int currentCombo = m_comboManager.CurrentCombo;

        // コンボが変わってないなら何もしない
        if (currentCombo == m_prevCombo) return;

        m_prevCombo = currentCombo;

        // 0以下なら生成しない
        if (currentCombo <= 0) return;

        int index = currentCombo - 1;
        if (index < 0 || index >= m_UIPrefabs.Length) return;

        GameObject prefab = m_UIPrefabs[index];
        if (prefab == null) return;

        Vector3 spawnPos = transform.position;
        Quaternion spawnRot = prefab.transform.rotation;

        // 親なし
        Instantiate(prefab, spawnPos, spawnRot);
    }
}
