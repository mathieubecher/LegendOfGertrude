using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrik : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Transform centerOfMass;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = Vector3.forward;
    }

    void FixedUpdate()
    {
        
    }
}
