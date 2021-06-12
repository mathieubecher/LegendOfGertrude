using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class JumpMob : StateMachineBehaviour
{
    private Mob _mob;
    [SerializeField] private AnimationCurve verticalPosition;
    [SerializeField] private AnimationCurve horizontalSpeed;

    private float _timer;
    private float _previousHeight;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _previousHeight = 0.0f;
        _mob = animator.GetComponent<Mob>();
        _mob.animation.ResetTrigger("Jump");
        _mob.animation.SetTrigger("Jump");
        _mob.rigidbody.velocity = Vector3.zero;
        animator.SetBool("Jump", true);
        animator.transform.rotation = Quaternion.LookRotation(
            (_mob.target.position - animator.transform.position).ProjectOntoPlane(Vector3.up));
        _timer = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        float height = verticalPosition.Evaluate(_timer);
        _mob.rigidbody.velocity = _mob.transform.forward * horizontalSpeed.Evaluate(_timer) +
                                  Vector3.up * (height -_previousHeight)/Time.deltaTime;
        _previousHeight = height;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
