using UnityEngine;

public class CameraFollow_NoRotate : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -8);
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private string playerTag = "Player";

    private Transform target;

    void LateUpdate()
    {
        // まだプレイヤーがいなければ探す
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null)
            {
                target = player.transform;
            }
            return;
        }

        // 位置だけ追従（回転は一切使わない）
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
