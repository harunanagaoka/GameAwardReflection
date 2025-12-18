using UnityEngine;

public class AttackTelegraphing : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject Telegraph;

    private bool isTracking = true;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player オブジェクトが見つかりません");
        }
    }

    // Update is called once per frame
    void Update()
    {
               if (isTracking)
               {
                   // プレイヤーを追尾
                   Telegraph.transform.position = Player.transform.position;
               }
    }
    // 外部から呼ぶ：追尾停止
    public void StopTracking()
    {
        isTracking = false;
    }

    // 外部から呼ぶ：現在位置を返す
    public Vector3 GetLockedPosition()
    {
        return Telegraph.transform.position;
    }
}
