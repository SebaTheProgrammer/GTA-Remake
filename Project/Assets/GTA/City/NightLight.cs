using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLight : MonoBehaviour
{
    [SerializeField]
    private TimeManager TimeM;

    [SerializeField]
    private GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        TimeM = FindObjectOfType<TimeManager>();
        Light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeM.CanSleep())
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }
    }
}
