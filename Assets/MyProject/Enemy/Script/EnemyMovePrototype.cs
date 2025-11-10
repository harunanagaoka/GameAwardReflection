using UnityEngine;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyMovePrototype : MonoBehaviour
{
    [SerializeField]
    private GameObject m_attack;

    //[SerializeField] 
    //private Vector3 m_atkOffset = Vector3.zero;

    [SerializeField]
    private float m_attackInterval = 0;

    private float m_time = 0;

    private EnemyEvents m_enemyEvents;

    private void Start()
    {
        m_enemyEvents = GetComponent<EnemyEvents>();
        m_enemyEvents.OnAttack.AddListener(Attack);
    }

    void Update()
    {
        m_time += Time.deltaTime;

        if(m_time > m_attackInterval)
        {
            m_enemyEvents.OnAttack?.Invoke();
            m_time = 0;
        }
    }

    private void Attack()
    {
        Instantiate(m_attack, transform.position, transform.rotation, transform);
    }
}
