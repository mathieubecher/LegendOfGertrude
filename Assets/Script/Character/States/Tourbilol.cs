using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Tourbilol : StateMachineBehaviour
{
    private Controller _controller;
    [SerializeField] private float _loadPhase = 1.5f;
    [SerializeField] private float _tourbilolPhase = 4f;
    [SerializeField] private float _stunPhase = 5f;
    [SerializeField] private Vector3 _swordOrientation;
    
    [SerializeField] private AudioClip _sourcePreTourbilol;
    private bool _playedSourcePreTourbilol;
    [SerializeField] private AudioClip _sourceTourbilol;
    private bool _playedSourceTourbilol;
    
    private Quaternion _swordOrientationStart;

    public float _timer = 0.0f;
    private int _toEjectAtStart = 5;
    private int _ejected = 0;
    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playedSourcePreTourbilol = false;
        _playedSourceTourbilol = false;
        _controller = animator.GetComponent<Controller>();
        _controller.rigidbody.velocity = Vector3.zero;
        _timer = 0.0f;
        _toEjectAtStart = 1 + _controller.sword.attachObjects.Count / 2;
        _ejected = 0;
        animator.SetBool("StartTourbilol", true);
        _controller.animator.SetBool("LoadTourbilol", true);
        _swordOrientationStart = _controller.sword.anchor.localRotation;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        _controller.sword.MoveRequest(_controller.transform);
        if (!_playedSourcePreTourbilol)
        {
            _playedSourcePreTourbilol = true;
            _controller.source.PlayOneShot(_sourcePreTourbilol);
        }

        if (_timer > _loadPhase)
        {
            if(!_controller.sword.destroy) _controller.sword.Tourbilol(true);
            if (!_playedSourceTourbilol)
            {
                _playedSourceTourbilol = true;
                _controller.source.PlayOneShot(_sourceTourbilol);
            }
            _controller.sword.anchor.rotation = Quaternion.LookRotation((_controller.sword.anchor.position - animator.transform.position).ProjectOntoPlane(Vector3.up));
            animator.SetBool("Tourbilol", true);
            animator.SetBool("Damage", false);
            _controller.animator.SetBool("Tourbilol", true);

            if (_timer > (1 + _ejected) * (_tourbilolPhase - _loadPhase) / (float) _toEjectAtStart + _loadPhase)
            {
                ++_ejected;
                _controller.sword.DetachLast();
            }
        }

        if (_timer > _tourbilolPhase)
        {
            _controller.sword.Tourbilol(false);
            _controller.sword.destroy = false;
            _controller.animator.SetBool("Tourbilol", false);
            _controller.sword.anchor.localRotation = _swordOrientationStart;
            
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
        _controller.sword.Tourbilol(false);
        _controller.animator.SetBool("LoadTourbilol", false);
        _controller.sword.anchor.localRotation = _swordOrientationStart;
        _controller.source.Stop();

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
