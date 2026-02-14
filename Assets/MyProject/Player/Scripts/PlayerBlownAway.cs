using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerEvents), typeof(Rigidbody))]
public class PlayerBlownAway : MonoBehaviour
{
    private enum BlownAwayState
    {
        None,
        BlownAway,
        Inertia
    }

    private BlownAwayState m_state = BlownAwayState.None;

    private bool m_isCanBlownAway = false;

    private bool m_isStun = false;

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

    [SerializeField, Tooltip("すり抜け防止用判定の細かさ")]
    private int m_maxStepCount = 15;

    [SerializeField, Tooltip("接触位置からのオフセット（プレイヤーが壁にめり込まないマージン）")]
    private float m_skinWidth = 0.05f;

    [SerializeField, Tooltip("防御しているときのコライダー")]
    private Collider m_playerDefenceCollider;

    [SerializeField, Tooltip("慣性移動の最大時間")]
    private float m_inertiaTime = 0.3f;

    private float m_sphereRadius = 0.3f;

    private float m_inertiaRemainingTime = 0f;

    public bool IsBlownAway => m_state == BlownAwayState.BlownAway;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerEvents = GetComponent<PlayerEvents>();

        m_playerEvents.OnDefence.AddListener(() => m_isCanBlownAway = true);
        m_playerEvents.OnDefenceEnd.AddListener(() => m_isCanBlownAway = false);
        m_playerEvents.OnStun.AddListener(() => m_isStun = true);
        m_playerEvents.OnStunEnd.AddListener(() => m_isStun = false);

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
        switch (m_state)
        {
            case BlownAwayState.BlownAway:
                BlowAwayMove();
                break;

            case BlownAwayState.Inertia:
                InertiaMove();
                break;
        }
    }

    public void StopBlownAway()
    {
        if (m_state != BlownAwayState.BlownAway)
            return;

        m_state = BlownAwayState.Inertia;
        m_inertiaRemainingTime = m_inertiaTime;

        m_playerEvents.OnBlownAwayCanceled?.Invoke();
    }

    public void BlowAway(Vector3 basePos, float force, float time)
    {
        if (m_state != BlownAwayState.None || !m_isCanBlownAway || m_isStun)
            return;

        m_state = BlownAwayState.BlownAway;

        Vector3 newDirection = transform.position - basePos;
        m_blowAwayDirection = newDirection.normalized;
        m_blowAwayForce = force;
        m_blowAwayTime = time;
        m_decayRate = CulculateDecayRate(force, time);

        m_playerEvents.OnBlownAway?.Invoke();
    }

    public void Reflect(Vector3 normal)
    {
        if (m_state == BlownAwayState.None)
            return;

        normal = normal.normalized;
        m_blowAwayDirection = Vector3.Reflect(m_blowAwayDirection, normal);
        m_blowAwayForce *= m_bounceMultiplier;
    }

    private void BlowAwayMove()
    {
        float totalDist = m_blowAwayForce * Time.fixedDeltaTime;

        int steps = Mathf.Clamp(Mathf.CeilToInt(totalDist / 0.1f), 1, m_maxStepCount);
        float stepDist = totalDist / steps;

        for (int i = 0; i < steps; i++)
        {
            if (DoSubstep(m_blowAwayDirection, stepDist))
                break;
        }

        ApplyDecay();
    }

    private void InertiaMove()
    {
        m_inertiaRemainingTime -= Time.fixedDeltaTime;

        if (m_inertiaRemainingTime <= 0f)
        {
            m_state = BlownAwayState.None;
            m_playerEvents.OnBlownAwayEnd?.Invoke();
            return;
        }

        float totalDist = m_blowAwayForce * Time.fixedDeltaTime;

        int steps = Mathf.Clamp(Mathf.CeilToInt(totalDist / 0.1f), 1, m_maxStepCount);
        float stepDist = totalDist / steps;

        for (int i = 0; i < steps; i++)
        {
            if (DoSubstep(m_blowAwayDirection, stepDist))
                break;
        }

        m_blowAwayForce *= m_decayRate;
    }


    private bool DoSubstep(Vector3 dir, float dist)
    {
        RaycastHit hit;
        Vector3 origin = transform.position;

        if (Physics.SphereCast(origin, m_sphereRadius, dir,
            out hit, dist + m_skinWidth, m_reflectionLayerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 pos = hit.point - dir * m_skinWidth;
            m_rigidbody.MovePosition(pos);
            Reflect(hit.normal);
            return true;
        }

        m_rigidbody.MovePosition(origin + dir * dist);
        return false;
    }

    private void ApplyDecay()
    {
        m_blowAwayForce *= m_decayRate;
        m_blowAwayTime -= Time.fixedDeltaTime;

        if (m_blowAwayTime <= 0f)
        {
            m_state = BlownAwayState.None;
            m_playerEvents.OnBlownAwayEnd?.Invoke();
        }
    }

    private float CulculateDecayRate(float initialForce, float time)
    {
        if (time <= 0f)
            return 0f;

        float stepCountF = time / Time.fixedDeltaTime;
        int steps = Mathf.Max(1, Mathf.RoundToInt(stepCountF));

        float decayRate = Mathf.Pow(m_targetForceRatio, 1f / steps);
        return Mathf.Clamp(decayRate, 0f, 1f);
    }
}//吹き飛ばしは敵の攻撃側から呼び出す。ぬるぽが怖いので。