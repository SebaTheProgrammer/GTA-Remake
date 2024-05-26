using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUi : MonoBehaviour
{
    [SerializeField]
    private GameObject ShelvesUi;

    private bool m_CanEat;

    void Start()
    {
        ShelvesUi.gameObject.SetActive(false);
    }
    void Update()
    {
        if (m_CanEat)
        {
            ShelvesUi.gameObject.SetActive(true);
        }
        else
        {
            ShelvesUi.gameObject.SetActive(false);
        }
    }

        void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!m_CanEat)
            {
                ShelvesUi.gameObject.SetActive(true);
                m_CanEat = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (m_CanEat)
            {
                ShelvesUi.gameObject.SetActive(false);
                m_CanEat = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
