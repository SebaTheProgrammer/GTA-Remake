using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    [SerializeField]
    private TimeManager TimeM;

    [SerializeField]
    private GameObject Light;
    [SerializeField]
    private GameObject Light2;

    void Start()
    {
        TimeM = FindObjectOfType<TimeManager>();
        Light.SetActive(false);
        Light2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeM.CanSleep())
        {
            Light.SetActive(true);
            Light2.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
            Light2.SetActive(false);
        }
    }
}
