using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyTimer");
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
