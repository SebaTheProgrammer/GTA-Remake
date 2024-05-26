using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWeapons : MonoBehaviour
{
    [SerializeField]
    private string m_WeaponsChangeButton;

    [SerializeField]
    private GameObject[] Weapons;

    [SerializeField]
    private int priceScrewdriver;
    [SerializeField]
    private int priceAxe;

    private int index;

    private bool hasBoughtSrewDirver;
    private bool hasBoughtAxe;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_WeaponsChangeButton))
        {
            if (index == 0)
            {
                ++index;
                Weapons[0].gameObject.SetActive(false);
                Weapons[1].gameObject.SetActive(true);
                Weapons[2].gameObject.SetActive(false);
                Weapons[3].gameObject.SetActive(false);
            }
            else if (index == 1 && hasBoughtSrewDirver)
            {
                ++index;
                Weapons[0].gameObject.SetActive(false);
                Weapons[1].gameObject.SetActive(false);
                Weapons[2].gameObject.SetActive(true);
                Weapons[3].gameObject.SetActive(false);
            }
            else if (index == 2 && hasBoughtAxe)
            {
                ++index;
                Weapons[0].gameObject.SetActive(false);
                Weapons[1].gameObject.SetActive(false);
                Weapons[2].gameObject.SetActive(false);
                Weapons[3].gameObject.SetActive(true);
            }
            else
            {
                index = 0;
                Weapons[0].gameObject.SetActive(true);
                Weapons[1].gameObject.SetActive(false);
                Weapons[2].gameObject.SetActive(false);
                Weapons[3].gameObject.SetActive(false);
            }
        }
    }

    public void BoughtSrewDriver()
    {
        if (!hasBoughtSrewDirver)
        {
            hasBoughtSrewDirver = true;
            Cash.Instance.AbstractCash(priceScrewdriver);
        }
    }
    public void BoughtAxe()
    {
        if (!hasBoughtAxe)
        {
            hasBoughtAxe = true;
            Cash.Instance.AbstractCash(priceAxe);
        }
    }
}
