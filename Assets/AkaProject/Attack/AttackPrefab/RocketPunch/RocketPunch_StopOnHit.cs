using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketPunch_StopOnHit : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 速度を完全に止める
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 物理停止
            rb.isKinematic = true;

            // 必要ならここで親にする
            // transform.SetParent(collision.transform);
        }
    }
}