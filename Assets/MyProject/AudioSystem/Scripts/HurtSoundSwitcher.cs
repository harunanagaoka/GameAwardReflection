using Unity.VisualScripting;
using UnityEngine;

public class HurtSoundSwitcher : MonoBehaviour
{
    private SEManager m_seManager;
    private PlayerEvents m_playerEvents;

    private void Start()
    {
        m_seManager = Object.FindFirstObjectByType<SEManager>();
        m_playerEvents.OnDamage.AddListener(OnDamageReceived);
    }

    public enum AttackType
    { 
        None,
        RocketPunch,
        ElectricGun,
        ElectricFieldAttack
    }

    private AttackType ConvertAttackType(string tag)
    {
        switch (tag)
        {
            case "RocketPunch":
                return AttackType.RocketPunch;
            case "Undefined":
                return AttackType.ElectricGun;
            case "ElectricFieldAttack":
                return AttackType.ElectricFieldAttack;
            default:
                return AttackType.None;
        }
    }

    private void PlayHurtSE(AttackType type)
    {
        switch (type)
        {
            case AttackType.RocketPunch:
                m_seManager.OnPlayOneShot(SEManager.SoundEffectName.BossPunchHit);
                break;
            case AttackType.ElectricGun:
                m_seManager.OnPlayOneShot(SEManager.SoundEffectName.BossElectricGun);
                break;
            case AttackType.ElectricFieldAttack:
                m_seManager.OnPlayOneShot(SEManager.SoundEffectName.PlayerNumb);
                break;
        }
    }

    private void OnDamageReceived(Collider other)
    {
        AttackType attackType = ConvertAttackType(other.tag);
        if (attackType != AttackType.None)
        {
            PlayHurtSE(attackType);
        }
    }

}
