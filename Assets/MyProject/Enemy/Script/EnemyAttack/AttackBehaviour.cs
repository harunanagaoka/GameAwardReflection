using UnityEngine;
using System.Collections;

public class AttackBehaviour : MonoBehaviour
{
    private AttackData m_attackData;

    public void Initialize(AttackData data)
    {
        m_attackData = data;
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        // çUåÇó\çê
        GameObject rangeObj = Instantiate(m_attackData.AttackRange, transform.position, transform.rotation, transform);

        yield return new WaitForSeconds(m_attackData.WarningDuration);

        // çUåÇó\çêè¡ãéÅAçUåÇî≠ê∂
        Destroy(rangeObj);

        GameObject attack = Instantiate(m_attackData.AttackPrefab, transform.position, transform.rotation, transform);

        if (attack.TryGetComponent<AttackHitBox>(out AttackHitBox hitBox))
        {
            hitBox.Initialize(transform.position, m_attackData.BlownAwayPower, m_attackData.BlownAwayTime, m_attackData.Damage);
        }

        yield return new WaitForSeconds(m_attackData.LifeTime);

        Destroy(gameObject);
    }
}
