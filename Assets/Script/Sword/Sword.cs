using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private bool _attack = true;
    private Rigidbody _rigidbody;
    public List<AttachObject> _attachObjects;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _attachObjects = new List<AttachObject>();
        _anchor.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attach(AttachObject other)
    {
        if (_attachObjects.Contains(other))
            return;
        
        _attachObjects.Add(other);
        
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
        transform.rotation = _anchor.rotation;
        
        /*
        Debug.DrawLine( _anchor.position, _anchor.position + Quaternion.LookRotation(previousCenter - _anchor.position ) * Vector3.forward);
        transform.Rotate(Vector3.up, Vector3.SignedAngle( previousPos - previousCenter, transform.position - previousCenter, Vector3.up) * Mathf.Min(1.0f,Mathf.Max(0.0f,_rigidbody.mass/100.0f)));
        */
        /*
        Vector3 previousCenter = transform.TransformPoint(_rigidbody.centerOfMass);
        transform.position = _anchor.position;
        if (Vector3.Angle(previousCenter - transform.position, transform.TransformPoint(_rigidbody.centerOfMass) - transform.position) < 0.1f) return;
        transform.rotation = Quaternion.LookRotation(previousCenter - transform.position);
        Debug.Log(Vector3.Angle(previousCenter - transform.position, transform.TransformPoint(_rigidbody.centerOfMass) - transform.position));
        Vector3 deltaPos = (previousCenter - transform.TransformPoint(_rigidbody.centerOfMass)) * (Mathf.Min(1.0f,Mathf.Max(0.0f,_rigidbody.mass/100.0f)));
        transform.position = transform.position + deltaPos;
        
        Vector3 fixActorPos = transform.position - (_anchor.position - actor.position);
        fixActorPos.y = actor.position.y;
        //actor.position = fixActorPos;
        */
    }
    
    void OnDrawGizmos()
    {
        if (_rigidbody == null) return;
        Gizmos.color = Color.red;
        
        Gizmos.DrawSphere(transform.TransformPoint(_rigidbody.centerOfMass), Mathf.Min(10,_rigidbody.mass/100.0f));
    }
}
