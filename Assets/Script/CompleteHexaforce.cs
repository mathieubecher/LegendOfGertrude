using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteHexaforce : MonoBehaviour
{
    private Controller _controller;

    [SerializeField] private List<GameObject> _frags;
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
    }
}
