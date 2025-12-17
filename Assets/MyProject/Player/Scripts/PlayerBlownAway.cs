using UnityEngine;
using System.Collections;

/*
 * 跳ね返しの挙動注意点
 * ・m_bounceMultiplierが1以上の場合、跳ねかえり状態の時間が長いほど速度が増幅し、すり抜けの可能性が上昇する */

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
    private float m_bounceMultiplier = 1.0f;

    private float m_blowAwayTime = 0f;

    [SerializeField, Tooltip("跳ね返り終わりまでに残る力（速度）の比率")]
    private float m_targetForceRatio = 0.01f;

    private float m_decayRate = 0.95f;

    [SerializeField, Tooltip("跳ね返り状態で与えるダメージ")]
    private int m_damage = 0;

    [SerializeField, Tooltip("ReflectionWall 判定に使うレイヤー")]
    private LayerMask m_reflectionLayerMask;

    [SerializeField,Tooltip("すり抜け防止用判定の細かさ")]
    private int m_maxStepCount = 15;

    [SerializeField, Tooltip("接触位置からのオフセット（プレイヤーが壁にめり込まないマージン）")]
    private float m_skinWidth = 0.05f;

    [SerializeField, Tooltip("防御しているときのコライダー")]
    private Collider m_playerDefenceCollider;

    private float m_sphereRadius = 0.3f;

    public bool IsBlownAway => m_isBlownAway;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnDefence.AddListener(() => m_isCanBlownAway = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isCanBlownAway = false);

        var sphere = m_playerDefenceCollider as SphereCollider;
        if (sphere != null)
        {
            Vector3 lossy = m_playerDefenceCollider.transform.lossyScale;
            float maxScale = Mathf.Max(Mathf.Abs(lossy.x), Mathf.Abs(lossy.y), Mathf.Abs(lossy.z));
            m_sphereRadius = sphere.radius * maxScale;
        }
    }


    void FixedUpdate()
    {
        if (m_isBlownAway)
        {
            BlowAwayMove();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_isBlownAway) return;

        //if (collision.gameObject.TryGetComponent<EnemyDamageable>(out EnemyDamageable enemy))
        //{
        //    enemy.TakeDamage(m_damage);
        //}
    }

    public void StopBlownAway()
    {
        m_isBlownAway = false;
    }


    public void BlowAway(Vector3 enemyPos, float force, float time)
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

    private void BlowAwayMove()
    {
        float totalDist = m_blowAwayForce * Time.fixedDeltaTime;

        int steps = Mathf.Clamp(Mathf.CeilToInt(totalDist / 0.1f), 1, m_maxStepCount);//1~10ステップ
        float stepDist = totalDist / steps;

        for (int i = 0; i < steps; i++)
        {
            if (DoSubstep(m_blowAwayDirection, stepDist))
                break; 
        }

        ApplyDecay();
    }

    private bool DoSubstep(Vector3 dir, float dist)
    {
        RaycastHit hit;
        Vector3 origin = transform.position;

        if (Physics.SphereCast(origin, m_sphereRadius, dir,
            out hit, dist + m_skinWidth, m_reflectionLayerMask, QueryTriggerInteraction.Ignore))
        {
            // 壁の直前に移動
            Vector3 pos = hit.point - dir * m_skinWidth;
            m_rigidbody.MovePosition(pos);

            // ★ここで必ず反射させる（方向もセットする）
            Reflect(hit.normal);

            return true;
        }

        // 壁に当たらないなら進む
        m_rigidbody.MovePosition(origin + dir * dist);
        return false;
    }

    private void ApplyDecay()
    {
        m_blowAwayForce *= m_decayRate;
        m_blowAwayTime -= Time.fixedDeltaTime;
        if (m_blowAwayTime <= 0f)
        {
            m_isBlownAway = false;
            m_playerEvents.OnBlownAwayEnd?.Invoke();
        }
    }

    /// <summary>
    /// time 秒後にinitialForceがほぼゼロになるような減衰率を返す。
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