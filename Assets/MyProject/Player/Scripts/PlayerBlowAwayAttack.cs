using UnityEngine;

public class PlayerBlowAwayAttack : MonoBehaviour
{
    [SerializeField]
    private int m_maxCombo = 5;

    [SerializeField] private float[] m_comboDamage;

    [SerializeField]
    private float m_maxIntervalTime = 0.1f;

    private float m_intervalTime = 0;

    private PlayerBlownAway m_blownAwayScript;

    private PlayerEvents m_playerEvents;

    private int m_atkCount = 0;

    public int CurrentCombo => m_atkCount;
   

    private void Start()
    {
        m_blownAwayScript = GetComponent<PlayerBlownAway>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnBlownAwayCanceled.AddListener(ResetCombo);
        m_playerEvents.OnBlownAwayEnd.AddListener(ResetCombo);

        ResetCombo();
    }

    private void Update()
    {
        CountIntervalTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_blownAwayScript.IsBlownAway || 0 < m_intervalTime)
        {
            return;
        }

        if (collision.collider.CompareTag("Enemy"))
        {


            if(collision.gameObject.TryGetComponent<EnemyDamageable>(out EnemyDamageable damageable))
            {
                int atkcount = Mathf.Min(m_atkCount, m_comboDamage.Length);
                damageable.TakeDamage(m_comboDamage[atkcount]);
            }

            m_atkCount = Mathf.Min(m_atkCount + 1, m_maxCombo + 1);
            m_playerEvents.OnComboStateChanged.Invoke(true);
            SetAttackInterval();
            
        }
    }

    private void SetAttackInterval()
    {
        m_intervalTime = m_maxIntervalTime;
    }

    private void CountIntervalTime()
    {
        if(m_intervalTime <= 0)
        {
            return;
        }

        m_intervalTime -= Time.deltaTime;

        if(m_intervalTime < 0)
        {
            m_intervalTime = 0;
        }
    }

    public void ResetCombo()
    {
        m_atkCount = 0;
        m_playerEvents.OnComboStateChanged.Invoke(false);
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
