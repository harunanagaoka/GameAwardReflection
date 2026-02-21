using System;
using UnityEngine;
using System.Collections;

public class VisualCuePlayerInTutorialScene : MonoBehaviour
{
    [SerializeField]
    FadeEffect m_fadeEffectPlayer;

    public event Action OnSceneEnterVisualCompleted = delegate { };
    public event Action OnSceneExitVisualCompleted = delegate { };

    private void Start()
    {
        StartCoroutine(PlaySceneEnterEffects());
    }

    private IEnumerator PlaySceneEnterEffects()
    {
        yield return StartCoroutine(m_fadeEffectPlayer.Fade(false));//フェードイン終了まで待つ

        OnSceneEnterVisualCompleted?.Invoke();
    }

    public void PlaySceneExitEffects()
    {
        StartCoroutine(PlaySceneExitEffectsCoroutine());
    }

    private IEnumerator PlaySceneExitEffectsCoroutine()
    {
        yield return StartCoroutine(m_fadeEffectPlayer.Fade(true));
        OnSceneExitVisualCompleted?.Invoke();
    }
}
