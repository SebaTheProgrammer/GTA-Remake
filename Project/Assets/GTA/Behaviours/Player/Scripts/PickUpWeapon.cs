using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerBat;

    [SerializeField]
    private GameObject GroundBat;

    [SerializeField]
    private GameObject PickUpUI;

    [SerializeField]
    private GameObject InventoryUI;

    private bool m_CanPickUp;
    private bool m_HasPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer m = PlayerBat.GetComponent<MeshRenderer>();
        m.enabled = false;
        PickUpUI.SetActive(false);
        GroundBat.SetActive(true);
        InventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& m_CanPickUp&& !m_HasPickedUp)
        {
            MeshRenderer m = PlayerBat.GetComponent<MeshRenderer>();
            m.enabled = true;

            GroundBat.SetActive(false);
            PickUpUI.SetActive(false);
            InventoryUI.SetActive(true);
            m_HasPickedUp = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && m_HasPickedUp==false)
        {
            if (!m_CanPickUp)
            {
                m_CanPickUp = true;
                PickUpUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (m_CanPickUp)
            {
                m_CanPickUp = false;
                PickUpUI.SetActive(false);
            }
        }
    }
}
