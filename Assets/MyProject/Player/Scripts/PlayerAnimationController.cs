using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator m_animator;

    private PlayerEvents m_playerEvents;

    private readonly int m_standingHash = Animator.StringToHash(AnimationNames.Standing);
    private readonly int m_runningHash = Animator.StringToHash(AnimationNames.Running);
   // private readonly int m_attackHash = Animator.StringToHash(AnimationNames.Attack);
    private readonly int m_damageHash = Animator.StringToHash(AnimationNames.Damage);

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_playerEvents = GetComponentInParent<PlayerEvents>();

        m_playerEvents.OnStartMove.AddListener(PlayRunning);

       // m_playerEvents.OnAttack.AddListener(PlayAttack);
        m_playerEvents.OnDamage.AddListener(PlayDamage);
        m_playerEvents.OnStop.AddListener(PlayStanding);

    }

    private void PlayRunning()
    {
        m_animator.Play(m_runningHash);
    }

    private void PlayStanding()
    {
        m_animator.Play(m_standingHash);
    }

    private void PlayAttack()
    {
      //  m_animator.Play(m_attackHash);
    }

    private void PlayDamage()
    {
        m_animator.Play(m_damageHash);
    }
}
