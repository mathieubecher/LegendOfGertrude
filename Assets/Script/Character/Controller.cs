using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private Sword _sword;
    
    private Rigidbody _rigidbody;
    private Vector2 _tilt;
    

    [SerializeField] private AnimationCurve _angularSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(_tilt.magnitude) > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_tilt.x, 0.0f, _tilt.y));
            
        }

        _rigidbody.velocity = transform.forward * _tilt.magnitude * _moveSpeed + Vector3.up * _rigidbody.velocity.y;
        
        
    }

    void LateUpdate()
    {
        _sword.MoveRequest(transform);
    }
    
    public void MoveInput(InputAction.CallbackContext context)
    {
        _tilt = context.ReadValue<Vector2>();
    }
}
