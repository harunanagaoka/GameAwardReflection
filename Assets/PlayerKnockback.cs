using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(PlayerEvents))]
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField, Tooltip("ノックバック距離（m）")]
    private float knockbackDistance = 1.2f;

    [SerializeField, Tooltip("ノックバックにかける時間（秒）")]
    private float knockbackTime = 0.12f;

    Rigidbody m_rigidbody;
    PlayerEvents m_playerEvents;

    Coroutine routine;
    bool isKnockbacking = false;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerEvents = GetComponent<PlayerEvents>();
    }

    /// <summary>
    /// direction : ノックバック方向（正規化不要）
    /// distance  : ノックバック距離（省略可）
    /// </summary>
    public void Play(Vector3 direction, float? distance = null)
    {
        // ★ 解決策①：ノックバック中は無視
        if (isKnockbacking)
            return;

        float dist = distance ?? knockbackDistance;
        routine = StartCoroutine(KnockbackCoroutine(direction, dist));
    }

    IEnumerator KnockbackCoroutine(Vector3 dir, float distance)
    {
        isKnockbacking = true;
        m_playerEvents.OnBlownAway?.Invoke();

        dir.y = 0f;
        dir.Normalize();

        float remaining = distance;
        float speed = distance / knockbackTime;

        while (remaining > 0f)
        {
            float moveDist = speed * Time.fixedDeltaTime;
            moveDist = Mathf.Min(moveDist, remaining);

            m_rigidbody.MovePosition(
                m_rigidbody.position + dir * moveDist
            );

            remaining -= moveDist;
            yield return new WaitForFixedUpdate();
        }

        isKnockbacking = false;
        m_playerEvents.OnBlownAwayEnd?.Invoke();
    }
}
