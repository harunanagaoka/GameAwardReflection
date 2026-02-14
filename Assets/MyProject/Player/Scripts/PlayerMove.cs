using UnityEngine;

[RequireComponent(typeof(PlayerEvents),typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float m_baseVelocity = 0;

    [SerializeField]
    float m_defendingVelocity = 0;

    [SerializeField]
    private float rotationSpeed = 90f;

    float m_currentVelocity = 0;

    PlayerEvents m_playerEvents;

    Rigidbody m_rigidbody;

    private bool m_isCanMove = true;

    private bool m_isStun = false;

    //“ü—Í•ûŒüŽó‚¯Žæ‚è
    private Vector3 m_inputDirection;
    private float m_inputRotation;

    private void Start()
    {
        m_currentVelocity = m_baseVelocity;

        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnMoveRight.AddListener(() => m_inputDirection += Vector3.right);
        m_playerEvents.OnMoveLeft.AddListener(() => m_inputDirection += Vector3.left);
        m_playerEvents.OnMoveForward.AddListener(() => m_inputDirection += Vector3.forward);
        m_playerEvents.OnMoveBackward.AddListener(() => m_inputDirection += Vector3.back);
        m_playerEvents.OnTurnLeft.AddListener(() => m_inputRotation -= rotationSpeed);
        m_playerEvents.OnTurnRight.AddListener(() => m_inputRotation += rotationSpeed);
        m_playerEvents.OnBlownAway.AddListener(() => m_isCanMove = false);
        m_playerEvents.OnBlownAwayCanceled.AddListener(() => m_isCanMove = true);
        m_playerEvents.OnBlownAwayEnd.AddListener(() => m_isCanMove = true);
        m_playerEvents.OnStun.AddListener(() => m_isStun = true);
        m_playerEvents.OnStunEnd.AddListener(()=> m_isStun = false);
        m_playerEvents.OnStunEnd.AddListener(SetBaseVelocity);
        m_playerEvents.OnDefence.AddListener(SetDefendingVelocity);
        m_playerEvents.OnDefenceEnd.AddListener(SetBaseVelocity);

        m_rigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (m_isCanMove && !m_isStun)
        {

            // ˆÚ“®
            if (m_inputDirection != Vector3.zero)
            {
                Vector3 movement = m_inputDirection.normalized * m_currentVelocity * Time.fixedDeltaTime;
                m_rigidbody.MovePosition(transform.position + movement);
            }

            // ‰ñ“]
            if (m_inputRotation != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, m_inputRotation * Time.fixedDeltaTime, 0);
                m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
            }

        }

        // ƒŠƒZƒbƒg
        m_inputDirection = Vector3.zero;
        m_inputRotation = 0;
    }

    private void SetDefendingVelocity()
    {

        m_currentVelocity = m_defendingVelocity;
    }
    
    private void SetBaseVelocity()
    {
        m_currentVelocity = m_baseVelocity;
    }

    public void SetCanMove(bool canMove)
    {
        m_isCanMove = canMove;
    }
}


