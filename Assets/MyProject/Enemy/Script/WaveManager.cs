using UnityEngine;

//Ó–±‚ªƒKƒo‚¢A‰üC—\’èB
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private MainGameEvents m_mainGameEvents;

    private bool m_isCleared = false;

    public bool IsCleared => m_isCleared;

    void Start()
    {
        if (m_mainGameEvents == null)
        {
            m_mainGameEvents = FindAnyObjectByType<MainGameEvents>();

            if (m_mainGameEvents == null)
            {
                Debug.Log("MainGameEvents‚ðƒZƒbƒg‚µ‚Ä‚­‚¾‚³‚¢");
            }
        }
    }

    void Update()
    {
        if (!m_isCleared)
        {
            m_isCleared = CheckWaveClear();
        }
       
    }

    private bool CheckWaveClear()
    {
        if(transform.childCount <= 0)
        {
            m_mainGameEvents.OnGameClear?.Invoke();
            return true;
        }

        return false;
    }
}
