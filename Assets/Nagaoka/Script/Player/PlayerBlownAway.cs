using UnityEngine;

[RequireComponent(typeof(PlayerEvents), typeof(Rigidbody))]
public class PlayerBlownAway : MonoBehaviour
{
    private bool m_isBlownAway = false;
    private bool m_isCanBlownAway = false;

    private Rigidbody m_rigidbody;
    private PlayerEvents m_playerEvents;

    private Vector3 m_blowAwayDirection = Vector3.forward;
    private float m_blowAwayForce = 10f;

    [SerializeField]
    [Tooltip("’µ‚Ë•Ô‚éŽž‚Ì‰Á‘¬")]
    private float m_bounceMultiplier = 1.5f;

    private float m_blowAwayTime = 0f;

    [SerializeField]
    [Tooltip("‘¬“x‚ÌŒ¸Š—¦")]
    private float m_decayRate = 0.95f;

    [SerializeField]
    [Tooltip("’µ‚Ë•Ô‚èó‘Ô‚Å—^‚¦‚éƒ_ƒ[ƒW")]
    private int m_damage = 0;

    public bool IsBlownAway => m_isBlownAway;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isCanBlownAway = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isCanBlownAway = false);
    }

    
    void FixedUpdate()
    {

        if (m_isBlownAway)
        {
            BlowAwayMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isBlownAway) return;

        if (other.TryGetComponent<EnemyDamageable>(out EnemyDamageable enemy))
        {
            enemy.TakeDamage(m_damage);
        }
    }
    

    public void BlowAway(Vector3 dir,float force,float Time)
    {
        if (m_isBlownAway || !m_isCanBlownAway)
        {
            return;
        }

        m_isBlownAway = true;
        m_blowAwayDirection = dir;
        m_blowAwayForce = force;
        m_blowAwayTime = Time;
        m_playerEvents.OnBlownAway?.Invoke();
    }

    public void Reflect(Vector3 normal)
    {
        if (!m_isBlownAway)
        {
            return;
        }

        normal = normal.normalized;
        m_blowAwayDirection = Vector3.Reflect(m_blowAwayDirection, normal);

        m_blowAwayForce *= m_bounceMultiplier;
    }

    private  void BlowAwayMove()
    {
        m_rigidbody.MovePosition(transform.position + m_blowAwayForce * m_blowAwayDirection * Time.fixedDeltaTime);
        m_blowAwayForce *= m_decayRate;
        m_blowAwayTime -= Time.fixedDeltaTime;
        if(m_blowAwayTime < 0)
        {
            m_isBlownAway = false;
            m_playerEvents.OnBlownAwayEnd?.Invoke();
        }
    }

}//‚«”ò‚Î‚µ‚Í“G‚ÌUŒ‚‘¤‚©‚çŒÄ‚Ño‚·B‚Ê‚é‚Û‚ª•|‚¢‚Ì‚ÅB
