using UnityEngine;
using UnityEngine.Events;

public class EnemyEvents : MonoBehaviour
{
    //ƒAƒNƒVƒ‡ƒ“
    public UnityEvent OnAttack = new UnityEvent();

    //ó‘Ô•Ï‰»
    public UnityEvent OnDeath = new UnityEvent();
    public UnityEvent OnDamage = new UnityEvent();
    public UnityEvent<float> OnDamagePenalty = new UnityEvent<float>();
}