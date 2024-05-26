using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterBuys : MonoBehaviour
{
    [SerializeField]
    private GameObject Object;

    // Start is called before the first frame update
    void Start()
    {
        Object.SetActive(false);
    }

    // Update is called once per frame
    public void GetBought(int price)
    {
        if (Cash.Instance.GetCash() >= price)
        {
            Object.SetActive(true);
            Cash.Instance.AbstractCash(price);
        }
    }
}
