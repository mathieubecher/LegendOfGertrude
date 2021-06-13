using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class PanicChicken : StateMachineBehaviour
{
    [SerializeField] private float _panicSpeed = 5.0f;
    [SerializeField] private float _panicAngularSpeed = 5.0f;
    private Chicken _chicken;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chicken = animator.GetComponent<Chicken>();
        _chicken.animator.SetBool("Run", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_chicken.mob == null) return;
        Vector3 direction = -(_chicken.mob.position - animator.transform.position).ProjectOntoPlane(Vector3.up).normalized;
        animator.transform.rotation = Quaternion.RotateTowards( animator.transform.rotation,
            Quaternion.LookRotation(
                direction), _panicAngularSpeed * Time.deltaTime);
        
        _chicken.rigidbody.velocity = animator.transform.forward * _panicSpeed + Vector3.up * _chicken.rigidbody.velocity.y;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _chicken.animator.SetBool("Run", false);    
    }

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
