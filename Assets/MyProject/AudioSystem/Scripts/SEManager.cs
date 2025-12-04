using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] m_audioSources;

    [SerializeField]
    private AudioClip[] m_audioClips;

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
    public void OnPlayOneShot(SoundEffectName seNum)
    {
        m_audioSources[(int)seNum].PlayOneShot(m_audioClips[(int)seNum]);
    }
    public void OnPlay(SoundEffectName seNum)
    {
        m_audioSources[(int)seNum].clip = m_audioClips[(int)seNum];
        m_audioSources[(int)seNum].loop = true;
        m_audioSources[(int)seNum].Play();
    }
    public void OnStop(SoundEffectName seNum)
    {
        m_audioSources[(int)(seNum)].Stop();
    }
}