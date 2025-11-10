using UnityEngine;

[RequireComponent(typeof(PlayerEvents),typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float m_moveVelocity = 0;

    [SerializeField]
    private float rotationSpeed = 90f;

    PlayerEvents m_playerEvents;

    Rigidbody m_rigidbody;

    private bool isCanMove = true;

    //“ü—Í•ûŒüŽó‚¯Žæ‚è
    private Vector3 m_inputDirection;
    private float m_inputRotation;

    private void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        m_playerEvents.OnMoveRight.AddListener(() => m_inputDirection += Vector3.right);
        m_playerEvents.OnMoveLeft.AddListener(() => m_inputDirection += Vector3.left);
        m_playerEvents.OnMoveForward.AddListener(() => m_inputDirection += Vector3.forward);
        m_playerEvents.OnMoveBackward.AddListener(() => m_inputDirection += Vector3.back);
        m_playerEvents.OnTurnLeft.AddListener(() => m_inputRotation -= rotationSpeed);
        m_playerEvents.OnTurnRight.AddListener(() => m_inputRotation += rotationSpeed);

        m_playerEvents.OnBlownAway.AddListener(() => isCanMove = false);
        m_playerEvents.OnBlownAwayEnd.AddListener(() => isCanMove = true);

        m_rigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (isCanMove)
        {

            // ˆÚ“®
            if (m_inputDirection != Vector3.zero)
            {
                Vector3 movement = m_inputDirection.normalized * m_moveVelocity * Time.fixedDeltaTime;
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
}


