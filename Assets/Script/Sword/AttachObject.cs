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

    private bool _destroy = false;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Sword");
        collider = GetComponent<Collider>();
        _destroy = false;
    }

    void Start()
    {
        sword.Attach(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detach()
    {
        
        Vector3 forward = sword.transform.forward;
        transform.parent = null;
        collider.isTrigger = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.useGravity = true;
        _destroy = true;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = (forward + Vector3.up) * Vector3.Dot(forward, transform.position - sword.transform.position) * 3;
        
        StartCoroutine("DelayDestroy");

    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (!sword.attach || other.transform.gameObject.layer == LayerMask.NameToLayer("Character") || other.gameObject.TryGetComponent<AttachObject>(out _)) return;
        if (_destroy)
        {
            Destroy(other.transform.gameObject);
            return;
        }
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
