using UnityEngine;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_atkPrefab;

    [SerializeField]
    private Vector3 m_atkOffset = Vector3.zero;

    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnAttack.AddListener(Attack);
    }

    private void Attack()
    {
        Vector3 atkPos = transform.position + transform.TransformDirection(m_atkOffset);
        Instantiate(m_atkPrefab, atkPos, transform.rotation);
    }
}
