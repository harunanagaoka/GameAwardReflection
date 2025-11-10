using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyEvents))]
public class EnemyAttackController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_attack;

    [SerializeField]
    private GameObject m_attackRange; // UŒ‚”ÍˆÍ

    [SerializeField]
    private float m_attackInterval = 5f; 

    [SerializeField]
    private float m_warningDuration = 1f;

    private EnemyEvents m_enemyEvents;

    private void Start()
    {
        m_enemyEvents = GetComponent<EnemyEvents>();

        // ’èŠú“I‚ÈUŒ‚ŠJn
        StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_attackInterval);
            yield return StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        // UŒ‚—\
        GameObject rangeObj = Instantiate(m_attackRange, transform.position, transform.rotation, transform);

        yield return new WaitForSeconds(m_warningDuration);

        // 3. UŒ‚—\Á‹AUŒ‚”­¶
        Destroy(rangeObj);
        Instantiate(m_attack, transform.position, transform.rotation, transform);
        m_enemyEvents.OnAttack?.Invoke();
    }
}
