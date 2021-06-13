using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class AttackMob : StateMachineBehaviour
{
    private Mob _mob;
    [SerializeField] private float _damageTimer = 0.4f;
    [SerializeField] private float _damageValue = 0.5f;
    [SerializeField] private bool damage = false;
    
    [SerializeField] private float _damageDistance = 2.0f;

    private float timer = 0.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mob = animator.GetComponent<Mob>();
        _mob.animation.ResetTrigger("Attack");
        _mob.animation.SetTrigger("Attack");
        animator.SetBool("Attack", true);
        _mob.rigidbody.velocity = Vector3.zero;
        damage = false;
        timer = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > _damageTimer && !_mob.attach)
        {
            if (!damage && _damageDistance > (_mob.target.position - animator.transform.position).ProjectOntoPlane(Vector3.up)
                .magnitude)
            {
                damage = true;
                Controller target;
                if (_mob.target.TryGetComponent<Controller>(out target))
                {
                    if (target.life > 0) target.Damage(_damageValue);
                    if (target.life <= 0) _mob.ResetDetect();
                }
                else
                {
                    Destroy(_mob.target.gameObject);
                    _mob.ResetDetect();
                }
            }
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
