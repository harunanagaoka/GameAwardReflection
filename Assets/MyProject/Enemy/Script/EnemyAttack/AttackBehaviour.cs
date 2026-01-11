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

    //•ª—£‚µ‚½‚¢‚ªAŠù‚É‚±‚ê‘O’ñ‚Ìì‚è‚ğ‚µ‚Ä‚¢‚é‚Ì‚ÅA‚¢‚Á‚½‚ñƒXƒ‹[

    private IEnumerator AttackSequence()
    {
        // UŒ‚—\
        GameObject rangeObj = Instantiate(m_attackData.AttackRange, m_attackData.InitPos, m_attackData.AttackRange.transform.rotation, transform);

        yield return new WaitForSeconds(m_attackData.WarningDuration);

        // UŒ‚—\Á‹AUŒ‚”­¶
        Destroy(rangeObj);

        GameObject attack = Instantiate(m_attackData.AttackPrefab, m_attackData.InitPos, m_attackData.AttackPrefab.transform.rotation, transform);

        if (attack.TryGetComponent<AttackHitBox>(out AttackHitBox hitBox))
        {
            hitBox.Initialize(transform.position, m_attackData.BlownAwayPower, m_attackData.BlownAwayTime, m_attackData.TimePenalty);
        }
        else
        {
            var newhitBox = attack.AddComponent<AttackHitBox>();
            newhitBox.Initialize(transform.position, m_attackData.BlownAwayPower, m_attackData.BlownAwayTime, m_attackData.TimePenalty);
        }

        yield return new WaitForSeconds(m_attackData.LifeTime);

        Destroy(gameObject);
    }
}
