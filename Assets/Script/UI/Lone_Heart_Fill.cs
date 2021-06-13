using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lone_Heart_Fill : MonoBehaviour
{
    public int HeartValue;
    public GameObject HeartLeft;
    public GameObject HeartRight;
    // Start is called before the first frame update
    void Start()
    {
        HeartUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeartUpdate()
    {
        if (HeartValue >= 2)
        {
            HeartLeft.SetActive(true);
            HeartRight.SetActive(true);
        } else if (HeartValue == 1)
        {
            HeartLeft.SetActive(true);
            HeartRight.SetActive(false);
        } else if (HeartValue == 0)
        {
            HeartLeft.SetActive(false);
            HeartRight.SetActive(false);
        }
    }

}
