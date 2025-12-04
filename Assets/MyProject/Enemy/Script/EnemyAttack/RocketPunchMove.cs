using UnityEngine;

public class RocketPunchMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float m_delayTime;

    public float m_speed;
    private Vector3 m_direction;

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
    }

    public void Move()
    {
        //オブジェクトをm_directionの方向にm_speedの速さで移動させる
    }
}
