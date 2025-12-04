using UnityEngine;
using System.Collections;

public class ElectricFieldAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_arrartPrefab;

    [SerializeField]
    private GameObject m_attackPrefab;

    [SerializeField]
    private float m_arrartTime = 1f;

    [SerializeField]
    private float m_attackTime = 3f;

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        GameObject arrart = Instantiate(m_arrartPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(m_arrartTime);
        Destroy(arrart);

        GameObject attack = Instantiate(m_attackPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(m_attackTime);
        Destroy(attack);
    }

}
