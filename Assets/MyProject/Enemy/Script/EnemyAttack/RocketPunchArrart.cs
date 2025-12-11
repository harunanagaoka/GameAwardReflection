using UnityEngine;

public class RocketPunchArrart : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    Quaternion rotation;

    public float m_speed;
    private Vector3 m_direction;
    private Rigidbody rigid;

    private void Start()
    {
        Player = PlayerManager.Instance.Players[0];
        rigid = GetComponent<Rigidbody>();
        UpdateDirection();
    }
    private void FixedUpdate()
    {
       UpdateDirection();
    }

    private void UpdateDirection()
    {
        //1.スクリプトがアタッチされているオブジェクトの向きをプレイヤーの方向に向ける
        //2.m_directionをプレイヤーの方向に更新する

        rotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);    // 向きを回転するQuaternion
        transform.rotation = rotation;

    }
}
