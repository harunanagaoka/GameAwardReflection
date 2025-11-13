/*
 * 跳ね返しの挙動注意点
 * ・m_bounceMultiplierが1以上の場合、跳ねかえり状態の時間が長いほど速度が増幅し、すり抜けの可能性が上昇する */

using UnityEngine;

[RequireComponent(typeof(PlayerEvents), typeof(Rigidbody))]
public class PlayerBlownAway : MonoBehaviour
{
    private bool m_isBlownAway = false;
    private bool m_isCanBlownAway = false;

    private Rigidbody m_rigidbody;
    private PlayerEvents m_playerEvents;

    private Vector3 m_blowAwayDirection = Vector3.forward;
    private float m_blowAwayForce = 10f;

    [SerializeField, Tooltip("跳ね返る時の加速")]
    private float m_bounceMultiplier = 1.5f;

    private float m_blowAwayTime = 0f;

    [SerializeField, Tooltip("跳ね返り終わりまでに残る力（速度）の比率")]
    private float m_targetForceRatio = 0.01f;

    private float m_decayRate = 0.95f;

    [SerializeField, Tooltip("跳ね返り状態で与えるダメージ")]
    private int m_damage = 0;

    public bool IsBlownAway => m_isBlownAway;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isCanBlownAway = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isCanBlownAway = false);
    }

    
    void FixedUpdate()
    {

        if (m_isBlownAway)
        {
            BlowAwayMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isBlownAway) return;

        if (other.TryGetComponent<EnemyDamageable>(out EnemyDamageable enemy))
        {
            enemy.TakeDamage(m_damage);
        }
    }
    

    public void BlowAway(Vector3 enemyPos,float force,float time)
    {
        if (m_isBlownAway || !m_isCanBlownAway)
        {
            return;
        }

        m_isBlownAway = true;

        Vector3 newDirection = transform.position - enemyPos;
        m_blowAwayDirection = newDirection.normalized;
        m_blowAwayForce = force;
        m_blowAwayTime = time;
        m_decayRate = CulculateDecayRate(force, time);

        m_playerEvents.OnBlownAway?.Invoke();

        //追加案：m_blowAwayDirectionがゼロベクトルになった場合の処理入れてもいいかも
        //ただし確率が低いのでベータ版では省略しておく
    }

    public void Reflect(Vector3 normal)
    {
        if (!m_isBlownAway)
        {
            return;
        }

        normal = normal.normalized;
        m_blowAwayDirection = Vector3.Reflect(m_blowAwayDirection, normal);

        m_blowAwayForce *= m_bounceMultiplier;
        //跳ね返る時の増加率を対象によって変えたい場合は引数でbounceMultiplierを受け取るように変更する
    }

    private  void BlowAwayMove()
    {
        m_rigidbody.MovePosition(transform.position + m_blowAwayForce * m_blowAwayDirection * Time.fixedDeltaTime);
        m_blowAwayForce *= m_decayRate;
        m_blowAwayTime -= Time.fixedDeltaTime;
        if(m_blowAwayTime < 0)
        {
            m_isBlownAway = false;
            m_playerEvents.OnBlownAwayEnd?.Invoke();
        }
    }

    /// <summary>
    ///time 秒後にinitialForceがほぼゼロになるような減衰率を返す。
    /// </summary>
    private float CulculateDecayRate(float initialForce, float time)
    {
        if (time <= 0f)
        {
            return 0f;
        }

        // time秒で呼ばれるFixedUpdateの回数を算出しています
        float stepCountF = time / Time.fixedDeltaTime;
        int steps = Mathf.Max(1, Mathf.RoundToInt(stepCountF));

        // decayRate = steps回掛け算すると targetRatio になる値
        float decayRate = Mathf.Pow(m_targetForceRatio, 1f / steps);

        // 0以下や1以上にはならないように制限している
        decayRate = Mathf.Clamp(decayRate, 0f, 1f);

        return decayRate;
    }

}//吹き飛ばしは敵の攻撃側から呼び出す。ぬるぽが怖いので。
