using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateMachineBehaviour
{
    private Controller _controller;
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _angularSpeed = 90.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<Controller>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Math.Abs(_controller.tilt.magnitude) > 0.1f)
        {
            _controller.transform.rotation = Quaternion.RotateTowards(_controller.transform.rotation, Quaternion.LookRotation(new Vector3(_controller.tilt.x, 0.0f, _controller.tilt.y)),_angularSpeed * Time.deltaTime);
            
        }

        _controller.rigidbody.velocity = _controller.transform.forward * _controller.tilt.magnitude * _moveSpeed + Vector3.up * _controller.rigidbody.velocity.y;
        _controller.sword.MoveRequest(_controller.transform);
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
