using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBuilding : MonoBehaviour
{
    [SerializeField]
    private int m_IndexBuilding;

    [SerializeField]
    private string m_EnterButton;

    [SerializeField]
    private BuildingHandeler m_Handeler;

    [SerializeField]
    private GameObject EnterUi;

    private bool m_IsInBuilding;

    private bool m_CanEnter;

    // Update is called once per frame
    void Update()
    {
        if (m_CanEnter)
        {
            if (Input.GetKeyDown(m_EnterButton)&&!m_IsInBuilding)
            {
                m_Handeler.OpenBuildingMenu(m_IndexBuilding);
                m_IsInBuilding=true;
                EnterUi.SetActive(false);
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && Cash.Instance.HasBoughtBuilding(m_IndexBuilding))
        {
            m_CanEnter = true;
            EnterUi.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_CanEnter = false;
            m_Handeler.CloseBuildingMenu();
            m_IsInBuilding = false;
            EnterUi.SetActive(false);
        }
    }
}
