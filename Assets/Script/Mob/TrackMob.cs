using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class TrackMob : StateMachineBehaviour
{
    private Mob _mob;

    [SerializeField] private float _lostDist = 20.0f;
    [SerializeField] private float _trackSpeed = 7.0f;
    [SerializeField] private float _angularSpeed = 360.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mob = animator.GetComponent<Mob>();
        animator.SetFloat("Distance", (_mob.target.position - animator.transform.position).ProjectOntoPlane(Vector3.up).magnitude);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_mob.target == null) animator.SetBool("Target", false);
        else if ((_mob.target.position - animator.transform.position).magnitude > _lostDist)
        {
            _mob.target = null;
            animator.SetBool("Target", false);
        }
        else
        {
            animator.SetFloat("Distance", (_mob.target.position - animator.transform.position).ProjectOntoPlane(Vector3.up).magnitude);
            animator.transform.rotation = Quaternion.RotateTowards( animator.transform.rotation,
                Quaternion.LookRotation(
                    (_mob.target.position - animator.transform.position).ProjectOntoPlane(Vector3.up)), _angularSpeed * Time.deltaTime);
            _mob.rigidbody.velocity = animator.transform.forward * _trackSpeed + Vector3.up * _mob.rigidbody.velocity.y;
            
            
        }
        
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
