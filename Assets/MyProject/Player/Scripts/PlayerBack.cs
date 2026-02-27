using System.Collections;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("最小X座標")]
    private float MinRangeX = -10f;
    [SerializeField]
    [Tooltip("最小Z座標")]
    private float MinRangeZ = -10f;
    [SerializeField]
    [Tooltip("最大X座標")]
    private float MaxRangeX = 10f;
    [SerializeField]
    [Tooltip("最大Z座標")]
    private float MaxRangeZ = 10f;
    [SerializeField]
    [Tooltip("チェック間隔")]
    private float CheckInterval = 3f;
    [SerializeField]
    [Tooltip("戻し位置")]
    private Vector3 ReturnPosition = new Vector3(0f, 0f, 0f);
    [SerializeField]
    private GameObject player;

    private Vector3 playerPosition;


    void Start()
    {
        player = GameObject.Find("Player_WithAnimation") ?? GameObject.Find("Player_WithAnimation(Clone)");
        StartCoroutine(PlayerBackArea());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayerBackArea()
    {
        while (true)
        {
            if (player == null)
            {
                player = GameObject.Find("Player_WithAnimation") ?? GameObject.Find("Player_WithAnimation(Clone)");
            }

            yield return new WaitForSeconds(CheckInterval);
            playerPosition = player.transform.position;
            if (playerPosition.x < MinRangeX || playerPosition.x > MaxRangeX || playerPosition.z < MinRangeZ || playerPosition.z > MaxRangeZ)
            {
                // プレイヤーが範囲外にいる場合、元の位置に戻す
                player.transform.position = ReturnPosition; // 元の位置を指定
            }
            yield return null;
        }
    }
}
