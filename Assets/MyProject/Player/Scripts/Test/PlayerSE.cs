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
        m_playerEvents.OnBlownAwayCanceled.AddListener(PlayAttackClowSE);
        m_playerEvents.OnBlownAway.AddListener(PlayBlownAwaySE);
    }

    private void PlayAttackClowSE()
    {
        m_seManager.OnPlayOneShot((SoundEffectName.PlayerAttackClow));
    }
    private void PlayBlownAwaySE()
    {
        m_seManager.OnPlayOneShot((SoundEffectName.Reflection));
    }

}
