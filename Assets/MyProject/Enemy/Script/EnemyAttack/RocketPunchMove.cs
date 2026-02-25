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
    float m_moveTime = 0.5f;

    [SerializeField]
    float m_stopTime = 0.5f;

    [SerializeField]
    float m_forwardspeed = 1.0f;

    [SerializeField]
    float m_Xrotation = -15f;

    public float m_speed;

    private Vector3 m_direction;

    private Rigidbody rigid;

    private SEManager m_seManager;

    private int seCount = 0;

    private bool isMoving = true;

    private bool isUpMoving = false;

    private bool isOneShot = false;

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

        Invoke(nameof(Move), m_moveTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            Invoke(nameof(UpMove),m_stopTime);
        }
    }

    private void UpdateDirection()
    {
        //1.スクリプトがアタッチされているオブジェクトの向きをプレイヤーの方向に向ける
        //2.m_directionをプレイヤーの方向に更新する

        rotation = Quaternion.LookRotation(Player.transform.position - this.transform.position);    // 向きを回転するQuaternion
        transform.rotation = rotation;

    }
    private void UpMove()
    {
        isUpMoving = true;
    }

    public void Move()
    {

        if (isMoving)
        {
            transform.position += transform.forward * m_speed * Time.fixedDeltaTime;

            //SE再生
            if (m_seManager != null && seCount == 0)
            {
                m_seManager.OnPlayOneShot(SEManager.SoundEffectName.BossPunch);
                seCount++;
            }
        }
       
        if (isUpMoving)
        {
            if (!isOneShot)
            {
                transform.rotation *= Quaternion.Euler(m_Xrotation, 0, 0);
                isOneShot = true;
            }
           //transform.position += transform.up * m_upspeed * Time.fixedDeltaTime;
            transform.position += transform.forward * m_forwardspeed * Time.fixedDeltaTime;
        }
    }
}
