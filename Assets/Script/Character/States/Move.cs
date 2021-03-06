using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateMachineBehaviour
{
    private Controller _controller;
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _minAngularSpeed = 360.0f;
    [SerializeField] private float _maxAngularSpeed = 90.0f;
    [SerializeField] private float _debugAngularSpeed = 0f;
    [SerializeField] private float _debugMoveSpeed = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<Controller>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float massFactor =1 -  Mathf.Max(0.0f, Mathf.Min(1.0f, _controller.sword.mass / 100.0f));
        
        _debugAngularSpeed = _minAngularSpeed + (_maxAngularSpeed - _minAngularSpeed) * (1-massFactor);
        _debugMoveSpeed = _moveSpeed * massFactor;
        if (Math.Abs(_controller.tilt.magnitude) > 0.1f)
        {
            _controller.transform.rotation = Quaternion.RotateTowards(_controller.transform.rotation, Quaternion.LookRotation(new Vector3(_controller.tilt.x, 0.0f, _controller.tilt.y)),
                (_minAngularSpeed + (_maxAngularSpeed - _minAngularSpeed) * (1-massFactor)) * Time.deltaTime);
        }

        _controller.rigidbody.velocity = _controller.transform.forward * _controller.tilt.magnitude * _moveSpeed * massFactor + Vector3.up * _controller.rigidbody.velocity.y;
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
