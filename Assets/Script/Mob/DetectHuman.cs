using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHuman : MonoBehaviour
{
    public Mob mob;

    public List<Transform> targets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform SelectTarget()
    {
        for (int i = targets.Count - 1; i >= 0; --i)
        {
            Debug.Log("test + " + targets[i]);
            if (targets[i] == null || targets[i].gameObject == null) targets.RemoveAt(i);
        }
        if (targets.Count == 0) return null;
        
        Transform nearest = targets[0]; 
        foreach (var target in targets)
        {
            if (target.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                return target;
            }

            if ((nearest.position - transform.position).magnitude > (target.position - transform.position).magnitude)
                nearest = target;
        }

        return nearest;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (mob == null) return;
        targets.Add(other.transform);
        
        mob.DetectHuman(SelectTarget());
    }
    public void OnTriggerExit(Collider other)
    {
        if (mob == null) return;
        targets.Remove(other.transform);
    }
    
}
