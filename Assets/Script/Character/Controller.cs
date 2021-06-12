using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Sword sword;
    
    [SerializeField]private Animator _animation;
    private Animator _fsm;

    [HideInInspector] public Rigidbody rigidbody;
    public Vector2 tilt;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        _fsm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnim();
    }

    private void UpdateAnim()
    {
        _animation.SetFloat("Speed", rigidbody.velocity.ProjectOntoPlane(Vector3.up).magnitude);
    }

    void FixedUpdate()
    {
    }
    
    public void MoveInput(InputAction.CallbackContext context)
    {
        tilt = context.ReadValue<Vector2>();
    }
    public void AttackLeft(InputAction.CallbackContext context)
    {
        _fsm.SetBool("Attack", true);
        _animation.SetInteger("AttackState", 0);
    }
    public void AttackRight(InputAction.CallbackContext context)
    {
        _fsm.SetBool("Attack", true);
        _animation.SetInteger("AttackState", 1);
        
    }
}
