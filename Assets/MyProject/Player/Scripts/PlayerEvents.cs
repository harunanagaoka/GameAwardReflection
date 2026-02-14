using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    //à⁄ìÆån
    public UnityEvent OnStartMove;
    public UnityEvent OnMoveRight;
    public UnityEvent OnMoveLeft;
    public UnityEvent OnMoveForward;
    public UnityEvent OnMoveBackward;
    public UnityEvent OnTurnLeft;
    public UnityEvent OnTurnRight;
    public UnityEvent OnStop;

    //ÉAÉNÉVÉáÉì
    public UnityEvent OnAttack;
    public UnityEvent OnDefence;
    public UnityEvent OnDefenceBleaked;
    public UnityEvent OnDefenceEnd;

    //èÛë‘ïœâª
    public UnityEvent OnDeath;
    public UnityEvent OnBlownAway;
    public UnityEvent OnBlownAwayCanceled;
    public UnityEvent OnBlownAwayEnd;
    public UnityEvent OnStun;
    public UnityEvent OnStunEnd;
    public UnityEvent OnDamage;
    public UnityEvent<float> OnDamagePenalty;
    
}
