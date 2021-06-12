using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbilol : StateMachineBehaviour
{
    private Controller _controller;
    [SerializeField] private float _loadPhase = 1.5f;
    [SerializeField] private float _tourbilolPhase = 4f;
    [SerializeField] private float _stunPhase = 5f;

    public float _timer = 0.0f;
    private int _toEjectAtStart = 5;
    private int _ejected = 0;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<Controller>();
        _timer = 0.0f;
        _toEjectAtStart = 5 + _controller.sword.attachObjects.Count / 2;
        _ejected = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        _controller.sword.MoveRequest(_controller.transform);

        if (_timer > _loadPhase)
        {
            animator.SetBool("Tourbilol", true);

            if (_timer > (1 + _ejected) * (_tourbilolPhase - _loadPhase) / (float) _toEjectAtStart + _loadPhase)
            {
                ++_ejected;
                Debug.Log("Eject");
                _controller.sword.DetachLast();
            }
        }
        if (_timer > _stunPhase)
        {
            animator.SetBool("StartTourbilol", false);
            animator.SetBool("Tourbilol", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
