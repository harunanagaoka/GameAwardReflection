using UnityEngine;

public class DamageAnimEndBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.gameObject.SendMessage(
            "OnDamageAnimationFinished",
            SendMessageOptions.DontRequireReceiver);
    }
}
