using UnityEngine;

public class MissileMoving : MonoBehaviour
{
    private GameObject MissileArrart; // テレグラフの GameObject

    [SerializeField]
    public float speed = 10f;  // ミサイルの速度

    private Vector3 targetPosition; // 発射時のテレグラフの位置
    private Vector3 direction;
    public bool initialized = false; // 初期化フラグ

    void Start()
    {
        targetPosition = GameObject.FindWithTag("Alert").transform.position;

        // 記録した座標へ向かう方向ベクトル
        direction = (targetPosition - transform.position).normalized;
        // 見た目をターゲット方向へ向ける
        transform.rotation = Quaternion.LookRotation(direction);
        initialized = true;
    }

    void FixedUpdate()
    {
        if (!initialized) return;
        // その方向へ移動
        transform.position += direction * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject); // 目標地点に到達したらミサイルを破壊
        }


    }

    //public void SetTelegraph(GameObject telegraph)
    //{
    //    MissileArrart = telegraph;
    //}

}




