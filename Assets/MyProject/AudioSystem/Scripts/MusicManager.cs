using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //MusicはSEとはちがい一度に複数個重なるケースが起こりにくいため、ひとつだけ所持させています。
    private AudioSource m_audioSource;

    [SerializeField]
    private AudioClip[] m_audioClips;

    public enum MusicName
    {
        Main
    }

    // 各SEごとの音量を管理
    [Range(0f, 1f)]
    [SerializeField]
    private float[] m_musicVolumes = new float[3] { 1, 1, 1};

    private void Start()
    {
        m_audioSource = new AudioSource();
        m_audioSource = gameObject.AddComponent<AudioSource>();
        OnPlay(MusicName.Main);
    }

    public void OnPlay(MusicName musicNum)
    {
        OnStop();
        float volume = m_musicVolumes[(int)musicNum];
        m_audioSource.clip = m_audioClips[(int)musicNum];
        m_audioSource.volume = volume;
        m_audioSource.loop = true;
        m_audioSource.Play();

    }

    public void OnStop()
    {
        m_audioSource.Stop();
    }
}