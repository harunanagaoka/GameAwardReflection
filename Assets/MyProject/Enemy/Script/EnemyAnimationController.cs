using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private EnemyEvents m_enemyEvents;

    private Animator m_animator;

    void Start()
    {
        m_enemyEvents = GetComponentInParent<EnemyEvents>();
        m_animator = GetComponent<Animator>();
        m_enemyEvents.OnDamage.AddListener(SetDamage);
        SetIdle();
    }

    private void SetIdle()
    {
        m_animator.SetBool("Idle", true);
        m_animator.SetBool("Damage", false);
    }

    private void SetDamage()
    {
        m_animator.SetBool("Idle", false);
        m_animator.SetBool("Damage", true);
    }

    private void OnDamageAnimationFinished()
    {
        SetIdle();
    }
}
