using UnityEngine;
using static SEManager;

public class PlayerSE : MonoBehaviour
{
    //シーン上のAudioManagerをアタッチする
    [SerializeField]
    private SEManager m_seManager;

    //Playerのイベントを使います
    private PlayerEvents m_playerEvents;


    private void Start()
    {
        m_playerEvents = GetComponent<PlayerEvents>();
        var audioManager = GameObject.Find("AudioManager");
        m_seManager = audioManager.GetComponent<SEManager>();
        //m_playerEventsにPlayerEventsをGetComponentする処理
        //m_seManagerにSEManagerをGetComponentする処理
        m_playerEvents.OnDamage.AddListener(PlayDamageSE);
        m_playerEvents.OnBlownAway.AddListener(PlayBlownAwaySE);
    }

    private void PlayDamageSE()
    {
        m_seManager.OnPlayOneShot((SoundEffectName.OnDamage));
    }
    private void PlayBlownAwaySE()
    {
        m_seManager.OnPlayOneShot((SoundEffectName.OnBlownAway));
    }

}
