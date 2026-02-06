using UnityEngine;

public class CameraFollow_NoRotate : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -8);
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private string playerTag = "Player";

    [Header("Camera Clamp (World Position)")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Transform target;

    void LateUpdate()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null)
            {
                target = player.transform;
            }
            return;
        }

        Vector3 desiredPos = target.position + offset;

        // Åö Ç±Ç±Ç™í«â¡É|ÉCÉìÉg
        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.z = Mathf.Clamp(desiredPos.z, minZ, maxZ);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
