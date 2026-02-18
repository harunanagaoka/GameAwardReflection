using UnityEngine;

public class RocketPunchMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    //[SerializeField]
    //private float m_delayTime;

    [SerializeField]
    Quaternion rotation;

    public float m_speed;
    private Vector3 m_direction;

    private Rigidbody rigid;

    private SEManager m_seManager;

    private int seCount = 0;


    private void Start()
    {
        seCount = 0;
        Player = PlayerManager.Instance.Players[0];
        rigid = GetComponent<Rigidbody>();
        m_seManager = Object.FindFirstObjectByType<SEManager>();
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
        transform.position += transform.forward * m_speed * Time.fixedDeltaTime;

        //SE再生
<<<<<<< HEAD
        if (m_seManager != null && seCount ==0)
=======
        if (m_seManager != null && seCount == 0)
>>>>>>> Alpha/035
        {
            m_seManager.OnPlayOneShot(SEManager.SoundEffectName.BossPunch);
            seCount++;
        }
    }
}
