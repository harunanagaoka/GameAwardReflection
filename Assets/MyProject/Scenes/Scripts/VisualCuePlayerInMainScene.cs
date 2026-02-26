using System;
using UnityEngine;
using System.Collections;

public class VisualCuePlayerInMainScene : MonoBehaviour
{
    [SerializeField]
    FadeEffect m_fadeEffectPlayer;

    [SerializeField]
    private float m_debugDuration = 5.0f;

    public event Action OnSceneEnterVisualCompleted = delegate { };
    public event Action OnSceneExitVisualCompleted = delegate { };
    public event Action OnPhaseTransitionVisualCompleted = delegate { };
    public event Action OnAllPhaseFinishedVisualCompleted = delegate { };

    void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(PlaySceneEnterEffects());
    }


    void Update()
    {
        
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

    public void PlayTransitionEffect()
    {
        StartCoroutine(Test());
    }

    public void PlayBossDefeat()
    {
        StartCoroutine(BossDestroyTest());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(m_debugDuration);
        OnPhaseTransitionVisualCompleted?.Invoke();
    }

    private IEnumerator BossDestroyTest()
    {
        yield return new WaitForSeconds(m_debugDuration);
        OnAllPhaseFinishedVisualCompleted?.Invoke();
    }
}
