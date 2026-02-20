using UnityEngine;

public class EnemyPunchHit : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField, Tooltip("ノックバック距離")]
    private float knockbackDistance = 0.6f;

    [Header("Hit Control")]
    [SerializeField, Tooltip("1攻撃で1回だけヒットさせる")]
    private bool hitOncePerAttack = true;

    private bool hasHit;

    /// <summary>
    /// 攻撃開始時に呼ぶ（アニメイベントなど）
    /// </summary>
    public void ResetHit()
    {
        hasHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitOncePerAttack && hasHit) return;
        if (!other.CompareTag("Player")) return;

        PlayerKnockback knockback = other.GetComponent<PlayerKnockback>();
        if (knockback == null) return;

        // パンチが進んでいる方向へノックバック
        knockback.Play(transform.forward, knockbackDistance);

        hasHit = true;
    }
}
