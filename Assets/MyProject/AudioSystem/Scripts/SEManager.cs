using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource[] m_audioSources;

    [SerializeField]
    private AudioClip[] m_audioClips;

    [SerializeField] 
    private int m_audioSourceCount = 10;

    private int m_currentIndex = 0;

    public enum SoundEffectName
    {
        cofirm,
        whistle,
        pl_footSteps,
        pl_Jamp,
        pl_respawn,
        coin_fall,
        nCoin_get,
        bCoin_get,
        seaContact,
        CountDown
    }

    private void Awake()
    {
        m_audioSources = new AudioSource[m_audioSourceCount];
        for (int i = 0; i < m_audioSourceCount; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            m_audioSources[i] = source;
        }
    }

    public void OnPlayOneShot(SoundEffectName seNum)
    {
        var source = GetFreeAudioSource();

        source.PlayOneShot(m_audioClips[(int)seNum]);

        m_currentIndex++;
        if (m_currentIndex >= m_audioSources.Length)
        {
            m_currentIndex = 0;
        }
    }

    //使っていないAudioSourceを選んでいます。
    private AudioSource GetFreeAudioSource()
    {
        int startIndex = m_currentIndex;

        while (m_audioSources[m_currentIndex].isPlaying)
        {
            m_currentIndex = (m_currentIndex + 1) % m_audioSources.Length;
            if (m_currentIndex == startIndex)
                break;
        }

        AudioSource source = m_audioSources[m_currentIndex];
        m_currentIndex = (m_currentIndex + 1) % m_audioSources.Length;
        return source;
    }

    private void OnValidate()
    {
    }
}

//※今回のSEManagerは単発の再生に限定し、繰り返し再生する必要がある場合、MusicManagerで管理することにします。
//　SEManagerの仕組みが、「今使っていないAudioSourceを探しそれを使う」ため、単発とエンドレス再生を混在させた場合
//　エンドレスで再生している音源がAudioSourceの枠を圧迫するためです。
//public void OnPlay(SoundEffectName seNum)
//{
//    var source = GetFreeAudioSource();

//    source.clip = m_audioClips[(int)seNum];
//    source.loop = true;
//    source.Play();

//    m_currentIndex++;

//    if (m_currentIndex >= m_audioSources.Length)
//    {
//        m_currentIndex = 0;
//    }
//}
