using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private Vector3 m_basePosition;

    private float m_blownAwayPower = 1;

    private float m_blownAwayTime = 1;

    private float m_damage = 1;

    private BreakDefenceAttack m_breakDefence = null;

    private ActionStunAttack m_stunAttack = null;

    private SingleHitAttack m_singleHitAttack = null;



    public void Initialize(Vector3 basePosition,float blownAwayPower, float blownAwayTime,float damage)
    {
        m_basePosition = basePosition;
        m_blownAwayPower = blownAwayPower;
        m_blownAwayTime = blownAwayTime;
        m_damage = damage;

        if (TryGetComponent<ActionStunAttack>(out ActionStunAttack stun))
        {
            m_stunAttack = stun;
        }

        if (TryGetComponent<BreakDefenceAttack>(out BreakDefenceAttack breakDefence))
        {
            m_breakDefence = breakDefence;
        }

        if (TryGetComponent<SingleHitAttack>(out SingleHitAttack singleHit))
        {
            m_singleHitAttack = singleHit;
        }
    }

    private void Update()
    {
        //m_basePosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (m_breakDefence)
        {
            //防御キャンセル
            m_breakDefence.BreakDefence();
        }

        if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
        {
            if (!playerDamageable.CanDamage)
            {
                return;
            }

            playerDamageable.TakeDamage(m_damage);
            
        }

        if (m_stunAttack)
        {
            //スタン
            m_stunAttack.StartStunCoroutine();
        }

        if (other.TryGetComponent<PlayerBlownAway>(out PlayerBlownAway playerBlowAway))
        {
            playerBlowAway.BlowAway(transform.position, m_blownAwayPower, m_blownAwayTime);
            if (playerBlowAway.IsBlownAway)
            {
                return;
            }
        }

        if (m_singleHitAttack)
        {
            m_singleHitAttack.OnHit();
        }
        
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (!other.CompareTag("Player"))
    //    {
    //        return;
    //    }

    //    if (m_breakDefence)
    //    {
    //        //防御キャンセル
    //        m_breakDefence.BreakDefence();
    //    }

    //    if (other.TryGetComponent<PlayerDamageable>(out PlayerDamageable playerDamageable))
    //    {
    //        playerDamageable.TakeDamage(m_damage);
    //    }

    //    if (m_stunAttack)
    //    {
    //        //スタン
    //        m_stunAttack.StartStunCoroutine();
    //    }

    //    if (m_singleHitAttack)
    //    {
    //        m_singleHitAttack.OnHit();
    //    }
    //}
}
