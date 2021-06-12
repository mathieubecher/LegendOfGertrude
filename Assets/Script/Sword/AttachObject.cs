using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttachObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float mass{get{return _rigidbody.mass;}}
    public Vector3 centerOfMass{get{return _rigidbody.centerOfMass;}}
    
    public Sword sword;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Sword");
    }

    void Start()
    {
        sword.Attach(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Character") || other.gameObject.TryGetComponent<AttachObject>(out _)) return;
        other.transform.SetParent(sword.transform);
        
        Debug.Log(other.gameObject);
        
        AttachObject otherAttachObjet = other.gameObject.AddComponent<AttachObject>();
        otherAttachObjet.sword = sword;
        
        Rigidbody otherRigidBody;
        if (other.transform.TryGetComponent<Rigidbody>(out otherRigidBody))
        {
            otherRigidBody.isKinematic = true;
        }
    }
}