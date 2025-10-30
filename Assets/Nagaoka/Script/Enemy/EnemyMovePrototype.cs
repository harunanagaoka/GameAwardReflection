using UnityEngine;

public class EnemyMovePrototype : MonoBehaviour
{
    [SerializeField]
    private GameObject m_attack;

    [SerializeField] 
    private Vector3 m_atkOffset = Vector3.zero;

    [SerializeField]
    private float m_attackInterval = 0;

    private float m_time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;

        if(m_time > m_attackInterval)
        {
            Attack();
            m_time = 0;
        }
    }

    private void Attack()
    {
        Instantiate(m_attack, transform.position + m_atkOffset, Quaternion.identity);
    }
}
