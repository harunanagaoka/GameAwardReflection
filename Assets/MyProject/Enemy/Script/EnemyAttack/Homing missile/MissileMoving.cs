using UnityEngine;
using UnityEngine.WSA;

public class StraightMissile : MonoBehaviour
{
    private GameObject Player; // プレイヤーの GameObject
    public Transform Boss;     // ボスの Transform

    [SerializeField]
    public float speed = 10f;  // ミサイルの速度

    private Vector3 targetPosition; // 発射時のプレイヤー位置
    private Vector3 direction;

    void Start()
    {
        // 発射した瞬間のプレイヤー位置を記録
        Player = PlayerManager.Instance.Players[0];
        targetPosition = Player.transform.position;

        // 記録した座標へ向かう方向ベクトル
        direction = (targetPosition - transform.position).normalized;
        // 見た目をターゲット方向へ向ける
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(targetPosition, transform.position) > 0.3f)
        {
            // その方向へ移動
            transform.position += direction * speed * Time.deltaTime;
       }

    }
}
