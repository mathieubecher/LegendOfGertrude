using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class ChillChicken : StateMachineBehaviour
{
    private Chicken _chicken;
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _angularSpeed = 180.0f;
    private Vector3 _randomDir = Vector3.forward;
    private float _randonDuration = -1.0f;
    private float _nextRandonDuration;
    private float _timer;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _chicken = animator.GetComponent<Chicken>();
        _chicken.rigidbody.velocity = Vector3.zero;
        _nextRandonDuration = Random.Range(1, 5);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        if (_timer > _nextRandonDuration)
        {
            if (_randonDuration < 0)
            {
                _randonDuration = Random.Range(0.5f, 2.0f);
                _randomDir = new Vector3(Random.Range(-1.0f,1.0f), 0.0f, Random.Range(-1.0f,1.0f)).normalized;
                _chicken.animator.SetBool("Walk", true);
            }
            animator.transform.rotation = Quaternion.RotateTowards( animator.transform.rotation,
                Quaternion.LookRotation(
                    _randomDir), _angularSpeed * Time.deltaTime);
        
            _chicken.rigidbody.velocity = animator.transform.forward * _moveSpeed + Vector3.up * _chicken.rigidbody.velocity.y;
            _randonDuration -= Time.deltaTime;
            if (_randonDuration < 0)
            {
                _timer = 0;
                _nextRandonDuration = Random.Range(1, 5);
                _chicken.animator.SetBool("Walk", false);
                
            }
        }
        else 
            _chicken.rigidbody.velocity = Vector3.zero;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _chicken.animator.SetBool("Walk", false);    
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
