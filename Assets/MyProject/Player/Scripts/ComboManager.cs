using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private int m_maxCombo = 5;

    [SerializeField] 
    private float[] m_comboDamage;

    private PlayerEvents m_playerEvents;

    private int m_atkCount = 0;

    public int CurrentCombo => m_atkCount;

    private void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnBlownAwayCanceled.AddListener(ResetCombo);
        m_playerEvents.OnBlownAwayEnd.AddListener(ResetCombo);
        m_playerEvents.OnBlowAwayAttack.AddListener(CountCombo);

        ResetCombo();
    }

    private void CountCombo()
    {
        m_atkCount = Mathf.Min(m_atkCount + 1, m_maxCombo);
        m_playerEvents.OnComboStateChanged.Invoke(true);
    }

    public void ResetCombo()
    {
        m_atkCount = 0;
        m_playerEvents.OnComboStateChanged.Invoke(false);
    }

    public float GetComboDamage()
    {
        float damage = m_comboDamage[m_atkCount];

        return damage;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        EnsureArraySize();
    }
#endif

    private void EnsureArraySize()
    {
        if (m_maxCombo < 1)
        {
            m_maxCombo = 1;
        }

        if (m_comboDamage == null || m_comboDamage.Length != m_maxCombo)
        {
            float[] newArray = new float[m_maxCombo + 1];

            // 既存値をコピー
            if (m_comboDamage != null)
            {
                int copy = Mathf.Min(m_comboDamage.Length, newArray.Length);

                for (int i = 0; i < copy; i++)
                {
                    newArray[i] = m_comboDamage[i];
                }
            }

            m_comboDamage = newArray;
        }
    }
}
