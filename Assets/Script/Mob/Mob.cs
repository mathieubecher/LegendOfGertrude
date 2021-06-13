using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public DetectHuman detect;
    public Transform target;

    [HideInInspector] public Animator fsm;
    public Animator animation;

    [HideInInspector] public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        fsm = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        detect.mob = this;
    }

    // Update is called once per frame
    void Update()
    {
        animation.SetFloat("Speed", rigidbody.velocity.magnitude);
    }

    public void DetectHuman(Transform human)
    {
        target = human;
        fsm.SetFloat("Distance", (target.position - transform.position).ProjectOntoPlane(Vector3.up).magnitude);
        fsm.SetBool("Target", true);
    }
}