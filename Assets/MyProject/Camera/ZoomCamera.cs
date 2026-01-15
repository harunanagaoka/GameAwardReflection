using System.Collections;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomDistance = 2.0f;
    [SerializeField] private float zoomTime = 0.2f;
    [SerializeField] private float holdTime = 1.6f;

    private Vector3 defaultLocalPosition;
    private Coroutine zoomCoroutine;

    [SerializeField]
    private EnemyManager m_enemyManager;

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
    }

    private void GetEnemyEvent()
    {
        m_enemyEvent = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();
        m_enemyEvent.OnDamage.AddListener(PlayZoom);
    }

    public void PlayZoom()
    {
        
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        zoomCoroutine = StartCoroutine(ZoomRoutine());
    }

    private IEnumerator ZoomRoutine()
    {
        Vector3 zoomPos =
            defaultLocalPosition + targetCamera.transform.forward * zoomDistance + m_enemyManager.BossEnemy.transform.position;

        // ズームイン（timeScale 無視）
        yield return MoveCameraRealtime(defaultLocalPosition, zoomPos, zoomTime);

        // キープ（timeScale 無視）
        yield return new WaitForSecondsRealtime(holdTime);

        // ズームアウト（timeScale 無視）
        yield return MoveCameraRealtime(zoomPos, defaultLocalPosition, zoomTime);
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

