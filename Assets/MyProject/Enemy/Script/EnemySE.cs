using UnityEngine;
using static SEManager;

public class EnemySE : MonoBehaviour
{
    //シーン上のAudioManagerから取得する
    [SerializeField]
    private SEManager m_seManager;

    //Enemyのイベントを使います
    private EnemyEvents m_enemyEvents;

    private void Start()
    {

    }

    public void Initialize(EnemyEvents events)
    {
        m_enemyEvents = events;
        var audioManager = GameObject.Find("AudioManager");
        m_seManager = audioManager.GetComponent<SEManager>();
        m_enemyEvents.OnDamage.AddListener(PlayDamageSE);
    }

    //関数名は例なので好きに命名してください。
    private void PlayDamageSE()
    {
        //敵に攻撃を当てた時のSEを再生するコード
        m_seManager.OnPlayOneShot((SoundEffectName.OnEnemyDamage));
    }
}
