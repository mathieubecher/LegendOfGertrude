using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] public Controller controller;
    [SerializeField] public Transform anchor;
    [SerializeField] private List<TrailRenderer> _trails;
    [SerializeField] public GameObject ejectTrail;
    [SerializeField] public GameObject attachVFX;
    
    
    [SerializeField] public List<AudioClip> chicken;
    [SerializeField] public List<AudioClip> old;
    [SerializeField] public List<AudioClip> mob;
    [SerializeField] public List<AudioClip> gertrude;
    

    [SerializeField] private bool _attach = false;
    public bool destroy = false;
    public bool attach
    {
        get => _attach;
        set
        {
            foreach (var attachObject in attachObjects)
            {
                if(attachObject.collider != null)
                    attachObject.collider.isTrigger = !value;
            }
            _attach = value;
            if (_attach)
            {
                foreach (var trail in _trails) trail.enabled = true;
            }
            else foreach (var trail in _trails) trail.enabled = destroy;
        }
    }
    private Rigidbody _rigidbody;
    
    public float mass{get{return _rigidbody.mass;}}
    public Vector3 centerOfMass{get{return _rigidbody.centerOfMass;}}
    public List<AttachObject> attachObjects;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        attachObjects = new List<AttachObject>();
        controller = FindObjectOfType<Controller>();
    }

    public void Attach(AttachObject other)
    {
        if (attachObjects.Contains(other))
            return;
        
        attachObjects.Add(other);
        UpdateMass();
    }

    public void Tourbilol(bool start = true)
    {
        destroy = start;
        foreach (var trail in _trails) trail.enabled = start || attach;
        foreach (var attachObject in attachObjects)
        {
            attachObject.collider.isTrigger = !start;
        }
    }
    public bool DetachLast()
    {
        if (attachObjects.Count <= 1) return false;
        var attachObject = attachObjects.Last();
        attachObject.Detach();
        attachObjects.Remove(attachObject);
        UpdateMass();
        return true;
    }

    public void UpdateMass()
    {
        
        Vector3 sum = Vector3.zero;
        float mass = 0.0f;
        foreach (var attachObject in attachObjects)
        {
            sum += attachObject.transform.TransformPoint(attachObject.centerOfMass) * attachObject.mass;
            mass += attachObject.mass;
        }
        _rigidbody.centerOfMass = transform.InverseTransformPoint(sum/mass);
        _rigidbody.mass = mass;

    }
    public void MoveRequest(Transform actor)
    {
        Vector3 previousPos = transform.position;
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
        
        /*
        Vector3 previousCenter = transform.TransformPoint(_rigidbody.centerOfMass);
        
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

    public void Dead()
    {
        foreach (var attachObject in attachObjects)
        {
            attachObject.transform.parent = null;
            attachObject.gameObject.layer = LayerMask.NameToLayer("Dead");
            var rb = attachObject.GetComponent<Rigidbody>();
            attachObject.collider.isTrigger = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
