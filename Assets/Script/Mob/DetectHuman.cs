using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHuman : MonoBehaviour
{
    public Mob mob;
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
        if (mob == null) return;
        mob.DetectHuman(other.transform);
    }
    
}
