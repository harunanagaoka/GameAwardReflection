using UnityEngine;
using UnityEngine.Events;

public class MainGameEvents : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameClear;

    public UnityEvent OnPhaseTransitionStart;
    public UnityEvent OnPhaseTransitionEnd;

    public UnityEvent OnGameFinishPresentationEnd;
}
