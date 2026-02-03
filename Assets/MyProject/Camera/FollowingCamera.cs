using UnityEngine;

public class CameraFollow_NoRotate : MonoBehaviour
{
    [SerializeField] private Transform target; // プレイヤー
    [SerializeField] private Vector3 offset;   // ワールド基準のオフセット
    [SerializeField] private float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // ★ 回転を一切使わず、位置だけ追従
        Vector3 desiredPos = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}

