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
    public float life = 6.0f;
    public int hexaforce = 0;
    public List<Hexaforce> hexaforces;
    public void AddHexaforce(Hexaforce fragHexaforce)
    {
        if (hexaforces.Contains(fragHexaforce)) return;
        hexaforces.Add(fragHexaforce);
        
        fragHexaforce.GetComponent<Animator>().SetTrigger("Active");
    }
    [HideInInspector] public Rigidbody rigidbody;
    public Vector2 tilt;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        _fsm = GetComponent<Animator>();
        hexaforces = new List<Hexaforce>();
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
        if (context.performed) _fsm.SetBool("StartTourbilol", true);
        else if(context.canceled) _fsm.SetBool("StartTourbilol", false);
    }

    public void ResetAttack()
    {
        _fsm.SetBool("Attack", false);
    }

    public void ResetDamage()
    {
        _fsm.SetBool("Damage", false);
    }
    
    public void Damage(float damageValue)
    {
        life -= damageValue;
        if (life <= 0)
        {
            Dead();
        }
        else
        {
            _fsm.SetBool("Damage", true);
        }
    }

    private void Dead()
    {
        Debug.Log("Dead");
        _fsm.SetTrigger("Dead");
    }

    
}
