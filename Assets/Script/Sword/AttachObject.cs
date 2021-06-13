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
    private bool _destroyable = true;

    private GameObject _ejectTrail;

    private int _originalLayer;
    private Quaternion _originalRotation;

    private RigidbodyConstraints _originalConstraint;
    // Start is called before the first frame update
    void Awake()
    {
        
        _rigidbody = GetComponent<Rigidbody>();
        
        _originalLayer = gameObject.layer;
        _originalRotation = transform.rotation;
        _originalConstraint = _rigidbody.constraints;
        
        _destroyable = (gameObject.layer == LayerMask.NameToLayer("Mob"));
        if (_destroyable)
        {
            Mob originMob;
            if (TryGetComponent<Mob>(out originMob))
            {
                originMob.Attach();
                Destroy(originMob);
                Destroy(GetComponent<Animator>());
            }
        }
        gameObject.layer = LayerMask.NameToLayer("Sword");
        collider = GetComponent<Collider>();
        _destroy = false;
    }

    void Start()
    {
        AudioSource source = sword.controller.animator.GetComponent<AudioSource>();
        if(_originalLayer == LayerMask.NameToLayer("Mob"))
            source.PlayOneShot(sword.mob[Random.Range(0, sword.mob.Count)]);
        else if(_originalLayer == LayerMask.NameToLayer("PNJ"))
            source.PlayOneShot(sword.old[Random.Range(0, sword.old.Count)]);
        else if (_originalLayer == LayerMask.NameToLayer("Chicken"))
        {
            source.PlayOneShot(sword.chicken[Random.Range(0, sword.chicken.Count)]);
            GetComponent<Animator>().SetBool("Attach", true);
        }
        sword.Attach(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detach()
    {
        _ejectTrail = Instantiate(sword.ejectTrail, transform);
        Vector3 forward = sword.transform.forward;
        _ejectTrail.transform.rotation = Quaternion.LookRotation(-forward);
        transform.parent = null;
        _destroy = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = (forward + Vector3.up) * Mathf.Max(5.0f, Vector3.Dot(forward, transform.position - sword.transform.position) * 3);

        
        StartCoroutine("DelayDestroy");

    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(_ejectTrail);
        if(_destroyable)Destroy(gameObject);
        else
        {
            gameObject.layer = _originalLayer;
            transform.rotation = _originalRotation;
            _rigidbody.constraints = _originalConstraint;
            if (_originalLayer == LayerMask.NameToLayer("Chicken"))
            {
                GetComponent<Animator>().SetBool("Attach", false);
            }
            Destroy(this);
        }
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Hexaforce"))
        {
            FindObjectOfType<Controller>().AddHexaforce(other.collider.gameObject.GetComponent<Hexaforce>());
            return;
        }
        
        if ((!sword.attach && !sword.destroy && !_destroy) || other.transform.gameObject.layer == LayerMask.NameToLayer("Character") || other.gameObject.TryGetComponent<AttachObject>(out _)) return;
        Rigidbody otherRigidBody;
        if (sword.destroy || _destroy)
        {
            if (other.transform.TryGetComponent<Rigidbody>(out otherRigidBody))
            {
                Destroy(other.transform.gameObject);
            }
            return;
        }
        other.transform.SetParent(sword.transform);
        
        
        Vector3 center = sword.transform.forward;
        Vector3 centeredPos = Vector3.Dot(center, other.transform.position - sword.transform.position) * center + sword.transform.position;
        other.transform.position = Vector3.Lerp(other.transform.position,centeredPos, 0.8f);
        
        GameObject VFX = Instantiate(sword.attachVFX, sword.transform);
        VFX.transform.rotation = Quaternion.LookRotation(centeredPos - other.transform.position);
        VFX.transform.position = other.transform.position;
        AttachObject otherAttachObjet = other.gameObject.AddComponent<AttachObject>();
        otherAttachObjet.sword = sword;

        if (other.transform.TryGetComponent<Rigidbody>(out otherRigidBody))
        {
            otherRigidBody.isKinematic = true;
        }
    }

}
