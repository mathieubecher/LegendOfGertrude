using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteHexaforce : MonoBehaviour
{
    private Controller _controller;
    private bool inside = false;
    private bool end = false;
    [SerializeField] private List<GameObject> _frags;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        _controller = FindObjectOfType<Controller>();
        foreach (var frag in _frags)
        {
            frag.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var frag in _controller.hexaforces)
        {
            _frags[frag.id].SetActive(true);
        }

        if (_controller.hexaforces.Count == 6 && inside && !end)
        {
            Debug.Log("End");    
            animator.SetTrigger("Victory");
            end = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inside = true;
    }

    void OnTriggerExit(Collider other)
    {
        inside = false;
    }
}
