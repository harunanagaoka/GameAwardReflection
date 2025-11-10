using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    //ˆÚ“®Œn
    public UnityEvent OnMoveRight;
    public UnityEvent OnMoveLeft;
    public UnityEvent OnMoveForward;
    public UnityEvent OnMoveBackward;
    public UnityEvent OnTurnLeft;
    public UnityEvent OnTurnRight;

    //ƒAƒNƒVƒ‡ƒ“
    public UnityEvent OnAttack;
    public UnityEvent OnDefence;
    public UnityEvent OnDefenceEnd;

    //ó‘Ô•Ï‰»
    public UnityEvent OnDeath;
    public UnityEvent OnBlownAway;
    public UnityEvent OnBlownAwayEnd;
    public UnityEvent OnDamage;
    
}
