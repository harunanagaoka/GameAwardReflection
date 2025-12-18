using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //MusicはSEとはちがい一度に複数個重なるケースが起こりにくいため、ひとつだけ所持させています。
    private AudioSource m_audioSource;

    [SerializeField]
    private AudioClip[] m_audioClips;

    public enum MusicName
    {
        Main,
        Title,
        ResultFirst,
        ResultLoop
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void OnPlay(MusicName musicNum)
    {
        OnStop();
        m_audioSource.clip = m_audioClips[(int)musicNum];
        m_audioSource.loop = true;
        m_audioSource.Play();

    }

    public void OnStop()
    {
        m_audioSource.Stop();
    }
}