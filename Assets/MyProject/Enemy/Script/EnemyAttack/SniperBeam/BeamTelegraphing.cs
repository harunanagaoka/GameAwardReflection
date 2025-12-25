using TMPro;
using UnityEngine;

public class BeamTelegraphing : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject PlayerPosition;

    private Vector3 lockedDirection;

    private bool isTracking = true;
    void Start()
    {
        Player = PlayerManager.Instance.Players[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            // プレイヤーの方向を追尾

            Vector3 direction = (Player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    // 外部から呼ぶ：追尾停止
    public void StopTracking()
    {
        isTracking = false;
        lockedDirection = transform.forward;
    }

    public Vector3 GetLockedDirection() 
    {
        return lockedDirection; 
    }
}
