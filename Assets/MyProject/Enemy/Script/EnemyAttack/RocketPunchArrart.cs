using UnityEngine;

public class RocketPunchArrart : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [Header("Tracking")]
    [SerializeField] private float trackingTime = 2f;   // 追跡時間
    [SerializeField] private float rotateSpeed = 10f;


    private float timer;
    private bool isTracking = true;

    private Rigidbody rigid;
    private Animator animator;

    private void Start()
    {
        Player = PlayerManager.Instance.Players[0];
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isTracking)
        {
            timer += Time.deltaTime;

            UpdateDirection();

            if (timer >= trackingTime)
            {
                //StopTracking();
            }
        }
    }

    private void UpdateDirection()
    {
        if (Player == null) return;

        Vector3 dir = Player.transform.position - transform.position;
        dir.y = 0f; // 上下無視（向きだけ）

        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
            );
        }
    }

    //private void StopTracking()
    //{
    //    isTracking = false;

    //    // 溜めアニメ開始
    //    animator.SetTrigger("Charge");

    //    // 数秒後に消す
    //    Destroy(gameObject);
    //}
}
