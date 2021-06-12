using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Sword sword;
    
    public Animator animator;
    private Animator _fsm;
    public int attackInput;

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
        animator.SetFloat("Speed", rigidbody.velocity.ProjectOntoPlane(Vector3.up).magnitude);
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
        if (!context.performed) return;
        _fsm.SetBool("Attack", true);
        attackInput = 0;
    }
    public void AttackRight(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _fsm.SetBool("Attack", true);
        attackInput = 1;
        
    }
    
    public void Tourbilol(InputAction.CallbackContext context)
    {
        Debug.Log("test");
        if (context.performed) _fsm.SetBool("StartTourbilol", true);
        else if(context.canceled) _fsm.SetBool("StartTourbilol", false);
    }

    public void ResetAttack()
    {
        _fsm.SetBool("Attack", false);
    }
}
