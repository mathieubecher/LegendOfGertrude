using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private DetectMob _detect;
    [HideInInspector] public Rigidbody rigidbody;
    public Transform mob;
    public Animator animator;
    private Animator _fsm;
    
    // Start is called before the first frame update
    void Awake()
    {
        _fsm = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mob = _detect.mob;
        _fsm.SetBool("Panic", mob != null);
    }
}
