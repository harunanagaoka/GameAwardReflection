using UnityEngine;
using static MusicManager;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audioSources;

    [SerializeField]
    private AudioClip[] m_audioClips;

    public enum MusicName
    {
        Main,
        Title,
        ResultFirst,
        ResultLoop
    }

    public void OnPlay(MusicName musicNum)
    {
        OnStop();
        m_audioSources.clip = m_audioClips[(int)musicNum];
        m_audioSources.loop = true;
        m_audioSources.Play();

    }
    public void OnStop()
    {
        m_audioSources.Stop();
    }
}