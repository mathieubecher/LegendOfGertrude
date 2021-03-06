using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : StateMachineBehaviour
{
    private Controller _controller;

    private float _timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<Controller>();
        _controller.animator.SetTrigger("Dead");
        _controller.gameObject.layer = LayerMask.NameToLayer("Dead");
        _controller.rigidbody.velocity = Vector3.zero;
        if(_controller.sword.attach) _controller.sword.attach = false;
        Rigidbody rbSword = _controller.sword.GetComponent<Rigidbody>();
        rbSword.useGravity = true;
        rbSword.isKinematic = false;
        rbSword.constraints = RigidbodyConstraints.None;
        _controller.sword.Dead();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        if (_timer > 10)
        {
            SceneManager.LoadScene(0);
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
