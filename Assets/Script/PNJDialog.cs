using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJDialog : MonoBehaviour
{
    public float minDist = 5.0f;
    private Controller _controller;
    public Animator animator;

    public bool near =false;
    // Start is called before the first frame update
    void Start()
    {
        _controller = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!near && (_controller.transform.position - transform.position).magnitude < minDist)
        {
            animator.SetTrigger("NextToNPC");
            
        }
    }
}
