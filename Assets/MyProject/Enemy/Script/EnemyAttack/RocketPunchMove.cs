using UnityEngine;

public class RocketPunchMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    //[SerializeField]
    //private float m_delayTime;

    [SerializeField]
    Quaternion rotation;

    [SerializeField]
    float m_MoveTime = 0.5f;

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
        //if(m_delayTime > 0)
        //{
        //    m_delayTime -= Time.fixedDeltaTime;
        //    UpdateDirection();
        //    return;
        //}

        Invoke("Move", m_MoveTime);
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
       transform.position += transform.forward * m_speed * Time.fixedDeltaTime;

    }
}
