using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    private Controller _controller;
    public float _timer;
    private int _attackInput;
    
    public float endLeftState = 1.4f;
    public float endRightState = 1.4f;
    public float activeTime = 0.4f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<Controller>();
        _controller.rigidbody.velocity = Vector3.zero;
        _attackInput = _controller.attackInput;
        _controller.attackInput = -1;
        _timer = 0.0f;
        _controller.animator.SetInteger("AttackState", _attackInput);
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller.sword.MoveRequest(_controller.transform);
        _timer += Time.deltaTime;
        if (_timer > activeTime && !_controller.sword.attach)
        {
            _controller.sword.attach = true;
            
        }
        if (_timer > ((_attackInput == 0) ? endLeftState : endRightState))
            _controller.ResetAttack();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller.sword.attach = false;
        _controller.animator.SetInteger("AttackState", -1);
        if (_controller.attackInput >= 0)
        {
            animator.SetBool("Attack", true);
            if(_controller.attackInput == _attackInput)
                _controller.animator.Play(_controller.animator.GetCurrentAnimatorStateInfo(0).ToString(), 0, 0.0f);
        }
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
