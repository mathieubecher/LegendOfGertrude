using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttachObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Collider collider;
    public float mass{get{return _rigidbody.mass;}}
    public Vector3 centerOfMass{get{return _rigidbody.centerOfMass;}}
    
    public Sword sword;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Sword");
        collider = GetComponent<Collider>();
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
        if (!sword.attach || other.transform.gameObject.layer == LayerMask.NameToLayer("Character") || other.gameObject.TryGetComponent<AttachObject>(out _)) return;
        other.transform.SetParent(sword.transform);
        
        
        Vector3 center = sword.transform.forward;
        other.transform.position = Vector3.Lerp(other.transform.position,Vector3.Dot(center, other.transform.position - sword.transform.position) * center + sword.transform.position, 0.8f);
        
        AttachObject otherAttachObjet = other.gameObject.AddComponent<AttachObject>();
        otherAttachObjet.sword = sword;

        Rigidbody otherRigidBody;
        if (other.transform.TryGetComponent<Rigidbody>(out otherRigidBody))
        {
            otherRigidBody.isKinematic = true;
        }
    }
}
