using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool attack = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (attack)
        {
            Debug.Log("Enter");
            Rigidbody otherRigidBody;

            other.transform.SetParent(transform);
            if (other.contacts[0].otherCollider.TryGetComponent<Rigidbody>(out otherRigidBody))
            {
                Destroy(otherRigidBody);
            }
        }
    }
}
