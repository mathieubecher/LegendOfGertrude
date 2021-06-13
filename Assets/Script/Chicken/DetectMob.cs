using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMob : MonoBehaviour
{
    [SerializeField] private Chicken _chicken;
    [SerializeField] private List<Transform> _mobs;

    public Transform mob
    {
        get
        {
            if(_mobs.Count == 0) return null;
            Transform nearest = _mobs[0];
            foreach (var nearMob in _mobs)
            {
                if ((nearMob.position - transform.position).magnitude <
                    (nearest.position - transform.position).magnitude)
                    nearest = nearMob;
            }
            return nearest;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (_chicken == null) return;
        _mobs.Add(other.transform);
    }
    public void OnTriggerExit(Collider other)
    {
        if (_chicken == null) return;
        _mobs.Remove(other.transform);
    }
}
