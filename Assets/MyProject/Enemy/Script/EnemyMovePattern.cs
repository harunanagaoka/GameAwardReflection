using UnityEngine;

public abstract class EnemyMovePattern : MonoBehaviour
{
    protected Rigidbody m_rigidbody;

    protected Vector3 m_direction;

    protected float m_speed;
    
    protected bool m_isReflected = false;

    void Awake() 
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void OnReflected(Vector3 normal)
    {
        m_isReflected = true;
        normal = normal.normalized;

        m_direction = Vector3.Reflect(m_direction, normal);

        // ïKóvÇ»ÇÁë¨ìxÇ‡ëùâ¡
        // m_speed *= 1.2f; // îΩéÀÇÃÇΩÇ—Ç…20%ë¨Ç≠Ç∑ÇÈÇ»Ç«

        //Vector3 velocity = m_rigidbody.linearVelocity;
        //Vector3 reflectedVelocity = Vector3.Reflect(velocity, normal);
        //m_rigidbody.linearVelocity = reflectedVelocity * 4.0f;
    }

    public abstract void Initialize(Vector3 dir,float speed);
    public abstract void Move();

    public abstract void SetDirection(Vector3 dir);

    public abstract void SetSpeed(float spd);
}
