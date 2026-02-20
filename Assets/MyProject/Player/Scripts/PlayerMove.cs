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
    private Vector2 m_inputRotation;

    private void Start()
    {
        m_currentVelocity = m_baseVelocity;

        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnMoveRight.AddListener(() => m_inputDirection += Vector3.right);
        m_playerEvents.OnMoveLeft.AddListener(() => m_inputDirection += Vector3.left);
        m_playerEvents.OnMoveForward.AddListener(() => m_inputDirection += Vector3.forward);
        m_playerEvents.OnMoveBackward.AddListener(() => m_inputDirection += Vector3.back);
        m_playerEvents.OnRotate.AddListener(InputRotation);
        m_playerEvents.OnBlownAway.AddListener(() => m_isCanMove = false);
        m_playerEvents.OnBlownAwayCanceled.AddListener(() => m_isCanMove = true);
        m_playerEvents.OnBlownAwayEnd.AddListener(() => m_isCanMove = true);
        m_playerEvents.OnStun.AddListener(() => m_isStun = true);
        m_playerEvents.OnStunEnd.AddListener(()=> m_isStun = false);
        m_playerEvents.OnStunEnd.AddListener(SetBaseVelocity);
        m_playerEvents.OnDefence.AddListener(SetDefendingVelocity);
        m_playerEvents.OnDefenceEnd.AddListener(SetBaseVelocity);
        m_playerEvents.OnKnockback.AddListener(() => m_isCanMove = false);
        m_playerEvents.OnKnockback.AddListener(() => m_isCanMove = true);

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
            if (m_inputRotation != Vector2.zero)
            {
                Vector3 dir = new Vector3(m_inputRotation.x, 0f, m_inputRotation.y);
                Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
                m_rigidbody.MoveRotation(targetRot);
            }

        }

        // ƒŠƒZƒbƒg
        m_inputDirection = Vector3.zero;
        m_inputRotation = Vector2.zero;
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

    private void InputRotation(Vector2 rotate)
    {
        m_inputRotation = rotate;
    }
}


