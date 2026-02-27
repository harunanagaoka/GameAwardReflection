using System.Collections;
using UnityEngine;

public class ZoomShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private CameraFollow_NoRotate cameraFollow; // ★ 追加

    [Header("Zoom Settings")]
    [SerializeField] private float zoomDistance = 2.0f;
    [SerializeField] private float zoomTime = 0.2f;
    [SerializeField] private float holdTime = 0.3f;

    [Header("Shake Settings")]
    [SerializeField] private float shakePowerX = 0.15f;
    [SerializeField] private float shakePowerZ = 0.05f;

    [Header("Enemy")]
    [SerializeField] private EnemyManager m_enemyManager;

    private EnemyEvents m_enemyEvent;
    private Transform bossTransform;

    private Vector3 defaultLocalPosition;
    private Vector3 defaultParentPosition;
    private Transform cameraParent;

    private Coroutine effectCoroutine;
    private bool isFollowingBoss = false;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        cameraParent = targetCamera.transform.parent;

        defaultLocalPosition = targetCamera.transform.localPosition;
        if (cameraParent != null)
        {
            defaultParentPosition = cameraParent.position;
        }

        m_enemyManager.OnBossJoined += GetEnemyEvent;
    }

    private void OnDisable()
    {
        m_enemyManager.OnBossJoined -= GetEnemyEvent;

        if (m_enemyEvent != null)
        {
            m_enemyEvent.OnDamage.RemoveListener(PlayEffect);
        }
    }

    private void GetEnemyEvent()
    {
        bossTransform = m_enemyManager.BossEnemy.transform;

        m_enemyEvent = bossTransform.GetComponent<EnemyEvents>();
        m_enemyEvent.OnDamage.AddListener(PlayEffect);
    }

    private void LateUpdate()
    {
        if (!isFollowingBoss) return;
        if (bossTransform == null || cameraParent == null) return;

        // ★ 向きは触らず、位置(XZ)のみ追従
        cameraParent.position = new Vector3(
            bossTransform.position.x,
            defaultParentPosition.y,
            bossTransform.position.z
        );
    }

    public void PlayEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
        }

        effectCoroutine = StartCoroutine(ZoomShakeRoutine());
    }

    private IEnumerator ZoomShakeRoutine()
    {
        // ===== 演出開始 =====
        cameraFollow.IsActive = false;   // ★ 通常追従OFF
        isFollowingBoss = true;

        Vector3 zoomPos =
            defaultLocalPosition + targetCamera.transform.forward * zoomDistance;

        // ① ズームイン
        yield return MoveCameraRealtime(defaultLocalPosition, zoomPos, zoomTime);

        // ② 揺れ
        float elapsed = 0f;
        while (elapsed < holdTime)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakePowerX, shakePowerX),
                0f,
                Random.Range(-shakePowerZ, shakePowerZ)
            );

            targetCamera.transform.localPosition = zoomPos + shakeOffset;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // ③ ズームアウト
        yield return MoveCameraRealtime(zoomPos, defaultLocalPosition, zoomTime);

        targetCamera.transform.localPosition = defaultLocalPosition;

        // ===== 演出終了 =====
        isFollowingBoss = false;
        cameraFollow.IsActive = true;    // ★ 通常追従ON
    }

    private IEnumerator MoveCameraRealtime(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            targetCamera.transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }

        targetCamera.transform.localPosition = to;
    }
}
