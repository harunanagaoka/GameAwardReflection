using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketPunchMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float m_delayTime;

    [SerializeField]
    Quaternion rotation;

    public float m_speed;
    private Vector3 m_direction;

    private Rigidbody rigid;


    
    private void Start()
    {
        Player = PlayerManager.Instance.Players[0];
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(m_delayTime > 0)
        {
            m_delayTime -= Time.fixedDeltaTime;
            UpdateDirection();
            return;
        }

        Move();
    }

    private void UpdateDirection()
    {
        //1.スクリプトがアタッチされているオブジェクトの向きをプレイヤーの方向に向ける
        //2.m_directionをプレイヤーの方向に更新する

        rotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);    // 向きを回転するQuaternion
        transform.rotation = rotation;

    }

    public void Move()
    {
        //オブジェクトをm_directionの方向にm_speedの速さで移動させる
        //rigid.MovePosition(transform.position + transform.forward * m_speed * Time.fixedDeltaTime);
       transform.position += transform.forward * m_speed * Time.fixedDeltaTime;
        if(Vector3.Distance(transform.position, Player.transform.position) < 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
