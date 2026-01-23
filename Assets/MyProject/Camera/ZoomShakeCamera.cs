using System.Collections;
using UnityEngine;

public class ZoomShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomDistance = 2.0f;
    [SerializeField] private float zoomTime = 0.2f;
    [SerializeField] private float holdTime = 0.3f;

    [Header("Shake Settings")]
    [SerializeField] private float shakePowerX = 0.15f;
    [SerializeField] private float shakePowerZ = 0.05f;

    private Vector3 defaultLocalPosition;
    private Coroutine effectCoroutine;

    [SerializeField] private EnemyManager m_enemyManager;
    private EnemyEvents m_enemyEvent;

    private void Awake()
    {
        m_enemyManager.OnBossJoined += GetEnemyEvent;

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        defaultLocalPosition = targetCamera.transform.localPosition;
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
        m_enemyEvent = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();
        m_enemyEvent.OnDamage.AddListener(PlayEffect);
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
        Vector3 zoomPos =
            defaultLocalPosition + targetCamera.transform.forward * zoomDistance;

        // ① ズームイン
        yield return MoveCameraRealtime(defaultLocalPosition, zoomPos, zoomTime);

        // ② 揺れ（ズーム位置を基準に）
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
