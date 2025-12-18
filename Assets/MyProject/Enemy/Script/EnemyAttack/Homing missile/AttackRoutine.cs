using UnityEngine;
using System.Collections;

public class AttackRoutine : MonoBehaviour
{
    [SerializeField] private AttackTelegraphing telegraph; // 追尾スクリプト
    [SerializeField] private SpownPoint spawnPoint;        // 生成位置
    [SerializeField] private GameObject missilePrefab;     // ミサイルのプレハブ

    [SerializeField] private float trackingTime = 1f;      // 追尾する時間

    void Start()
    {
        StartCoroutine(AttackFlow());
    }

    IEnumerator AttackFlow()
    {
        // ① Telegraph は Update で追尾中（何もしなくてOK）

        // ② trackingTime 秒間追尾
        yield return new WaitForSeconds(trackingTime);

        // ③ 追尾停止
        telegraph.StopTracking();

        // ④ 停止した位置を取得
        Vector3 lockedPos = telegraph.GetLockedPosition();

        // ⑤ ミサイルに Telegraph を渡す
        //GetComponent<MissileMoving>().SetTelegraph(telegraph.gameObject);
    }
}

