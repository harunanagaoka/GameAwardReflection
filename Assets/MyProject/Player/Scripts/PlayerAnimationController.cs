using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerEvents m_playerEvents;
    private Animator m_animator;
    void Start()
    {
        m_playerEvents = GetComponentInParent<PlayerEvents>();
        m_animator = GetComponent<Animator>();
        m_playerEvents.OnStartMove.AddListener(SetWalk);
        m_playerEvents.OnStop.AddListener(SetIdle);
        m_playerEvents.OnBlownAwayCanceled.AddListener(SetAttack);
        m_playerEvents.OnDamage.AddListener(SetDamage);
    }

    private void SetIdle()
    {
        m_animator.SetBool("Idle", true);
        m_animator.SetBool("Move", false);
        m_animator.SetBool("Damage", false);
        m_animator.SetBool("Attack", false);
    }
    
    private void SetAttack()
    {
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Move", false);
        m_animator.SetBool("Damage", false);
        m_animator.SetBool("Attack", true);
    }

    private void SetDamage()
    {
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Move", false);
        m_animator.SetBool("Damage", true);
        m_animator.SetBool("Attack", false);
    }

    private void SetWalk()
    {
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Move", true);
        m_animator.SetBool("Damage", false);
        m_animator.SetBool("Attack", false);
    }

    private void OnDamageAnimationFinished()
    {
        SetIdle();
    }
}
