using UnityEngine;

public class ResetAnimatorTrigger : StateMachineBehaviour
{
    [SerializeField] private string resetName;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(resetName);
    }
}