using UnityEngine;

public class PlayerOrientationController : MonoBehaviour
{
    private PlayerEvents m_playerEvents;

    void Awake()
    {
        m_playerEvents = GetComponent<PlayerEvents>();

        if(m_playerEvents == null)
        {
            Debug.LogError("PlayerEvents component not found in parent hierarchy.", this);
        }
    }

    void OnEnable()
    {
        if (m_playerEvents != null)
        {
            m_playerEvents.OnMoveForward.AddListener(OnMoveForwardPressed);
            m_playerEvents.OnMoveBackward.AddListener(OnMoveBackwardPressed);
            m_playerEvents.OnMoveRight.AddListener(OnMoveRightPressed);
            m_playerEvents.OnMoveLeft.AddListener(OnMoveLeftPressed);
        }
    }

    void OnDisable()
    {
        if (m_playerEvents != null)
        {
            m_playerEvents.OnMoveForward.RemoveListener(OnMoveForwardPressed);
            m_playerEvents.OnMoveBackward.RemoveListener(OnMoveBackwardPressed);
            m_playerEvents.OnMoveRight.RemoveListener(OnMoveRightPressed);
            m_playerEvents.OnMoveLeft.RemoveListener(OnMoveLeftPressed);
        }
    }

    private void OnMoveForwardPressed() => FaceDirection(Vector3.forward);
    private void OnMoveBackwardPressed() => FaceDirection(Vector3.back);
    private void OnMoveRightPressed() => FaceDirection(Vector3.right);
    private void OnMoveLeftPressed() => FaceDirection(Vector3.left);

    private void FaceDirection(Vector3 dir)
    {
        var flat = new Vector3(dir.x, 0f, dir.z);
        if (flat.sqrMagnitude <= 0f) return;
        var target = flat.normalized;
        transform.rotation = Quaternion.LookRotation(target, Vector3.up); // ‰Ÿ‚µ‚½uŠÔ‚ÉŒü‚¯‚é
    }
}