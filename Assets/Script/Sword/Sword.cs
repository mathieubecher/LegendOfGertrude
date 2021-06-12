using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private bool _attack = true;
    private Rigidbody _rigidbody;
    private Queue<AttachObject> _attachObjects;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _attachObjects = new Queue<AttachObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    /*
    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Character")) return;
        if (_attack)
        {
            Rigidbody otherRigidBody;
            
            AttachObject otherAttachObjet = other.gameObject.AddComponent<AttachObject>();
            Attach(otherAttachObjet);
            other.gameObject.layer = LayerMask.NameToLayer("Sword");
            other.transform.SetParent(transform);
            
            if (other.transform.TryGetComponent<Rigidbody>(out otherRigidBody))
            {
                otherRigidBody.isKinematic = true;
            }
        }
    }*/

    public void Attach(AttachObject other)
    {
        if (_attachObjects.Contains(other))
            return;
        
        _attachObjects.Enqueue(other);
        
        Vector3 sum = Vector3.zero;
        float mass = 0.0f;
        foreach (var attach in _attachObjects)
        {
            sum += attach.transform.TransformPoint(attach.centerOfMass) * attach.mass;
            mass += attach.mass;
        }
        _rigidbody.centerOfMass = transform.InverseTransformPoint(sum/mass);
        _rigidbody.mass = mass;

    }

    public void MoveRequest(Transform actor)
    {
        Vector3 previousCenter = transform.TransformPoint(_rigidbody.centerOfMass);
       
        Vector3 previousPos = transform.position;
        transform.position = _anchor.position;
        
        Debug.DrawLine( _anchor.position, _anchor.position + Quaternion.LookRotation(previousCenter - _anchor.position ) * Vector3.forward);
        transform.Rotate(Vector3.up, Vector3.SignedAngle( previousPos - previousCenter, transform.position - previousCenter, Vector3.up));

        Vector3 deltaCenter = previousCenter - transform.TransformPoint(_rigidbody.centerOfMass);
        Debug.DrawLine( actor.position, actor.position + deltaCenter * 4);
        //_anchor.position = _anchor.position + deltaCenter;
    }
    
    void OnDrawGizmos()
    {
        if (_rigidbody == null) return;
        Gizmos.color = Color.red;
        
        Gizmos.DrawSphere(transform.TransformPoint(_rigidbody.centerOfMass), Mathf.Min(10,_rigidbody.mass/100.0f));
    }
}
