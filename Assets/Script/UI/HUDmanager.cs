using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDmanager : MonoBehaviour
{
    public int Rupees;
    public int Hexaforce;
    public int Weight;
    public int Health;
    public Text RupeesValue;
    public Text HexaforceValue;
    public Text WeightValue;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    // Start is called before the first frame update
    void Start()
    {
        RupeesValue.text = Rupees.ToString();
        HexaforceValue.text = Hexaforce.ToString();
        WeightValue.text = Weight.ToString();
        Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
        Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
        Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
        Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        RupeesValue.text = Rupees.ToString();
        HexaforceValue.text = Hexaforce.ToString();
        WeightValue.text = Weight.ToString();

        if (Health == 0)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        } else if (Health == 1)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 1;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        } else if (Health == 2)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
           } else if (Health == 3)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 1;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        }  else if (Health == 4)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 0;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        } else if (Health == 5)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 1;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        } else if (Health >= 6)
        {
            Heart1.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart1.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart2.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart2.GetComponent<Lone_Heart_Fill>().HeartUpdate();
            Heart3.GetComponent<Lone_Heart_Fill>().HeartValue = 2;
            Heart3.GetComponent<Lone_Heart_Fill>().HeartUpdate();
        }

    }
}
