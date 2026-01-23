using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakePower = 0.15f;

    private Vector3 defaultLocalPosition;
    private Coroutine shakeCoroutine;

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
            m_enemyEvent.OnDamage.RemoveListener(PlayShake);
        }
    }

    private void GetEnemyEvent()
    {
        m_enemyEvent = m_enemyManager.BossEnemy.GetComponent<EnemyEvents>();
        m_enemyEvent.OnDamage.AddListener(PlayShake);
    }

    public void PlayShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 offset = new Vector3(
                Random.Range(-shakePower, shakePower), // X
                0f,                                    // Y は揺らさない
                Random.Range(-shakePower, shakePower)  // Z
            );

            targetCamera.transform.localPosition = defaultLocalPosition + offset;

            elapsed += Time.unscaledDeltaTime; // ヒットストップ対応
            yield return null;
        }

        targetCamera.transform.localPosition = defaultLocalPosition;
    }

}
