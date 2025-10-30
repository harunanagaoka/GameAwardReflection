using UnityEngine;

public class AttackDestroy : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime = 1.0f;

    private float m_time = 0;


    // Update is called once per frame
    void Update()
    {
       m_time += Time.deltaTime;

       if(m_time > m_lifeTime)
        {
            Destroy(gameObject);
            m_time = 0;
        }
    }
}
