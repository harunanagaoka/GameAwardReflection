using UnityEngine;

public class AlertDrop : MonoBehaviour
{
    [SerializeField] private float startHeight = 15f;
    [SerializeField] private float speed = 10f;

    private Vector3 targetPos;

    void Start()
    {
        // 今の位置をゴールにする
        targetPos = transform.position;

        // Yだけ高くする
        transform.position = new Vector3(
            targetPos.x,
            startHeight,
            targetPos.z
        );
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }
}