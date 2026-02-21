using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private Image m_fadeImage;

    [SerializeField, Tooltip("フェード時間")]
    private float m_fadeDuration = 1f;

    [SerializeField, Tooltip("暗転前の待ち時間")]
    private float m_waitBeforeFadeOut = 2f;

    [SerializeField, Tooltip("明転前(暗転中)の待ち時間")]
    private float m_waitBeforeFadeIn = 2f;

    void Awake()
    {
        m_fadeImage = GetComponent<Image>();
    }

    private void FadeInEffect()
    {
        StartCoroutine(Fade(false));
    }

    private void FadeOutEffect()
    {
        StartCoroutine(Fade(true));
    }

    public IEnumerator Fade(bool isFadeOut)
    {
        float startAlpha = m_fadeImage.color.a;
        float targetAlpha = 0;
        float time = 0;

        if (isFadeOut)
        {
            targetAlpha = 1;
            yield return new WaitForSeconds(m_waitBeforeFadeOut);
        }
        else
        {
            yield return new WaitForSeconds(m_waitBeforeFadeOut);
        }

        while (time < m_fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / m_fadeDuration);
            m_fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

    }
}
