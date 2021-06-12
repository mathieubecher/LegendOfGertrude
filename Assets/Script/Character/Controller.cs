using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Sword sword;
    
    [HideInInspector] public Rigidbody rigidbody;
    public Vector2 tilt;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate()
    {
    }
    
    public void MoveInput(InputAction.CallbackContext context)
    {
        tilt = context.ReadValue<Vector2>();
    }
}
