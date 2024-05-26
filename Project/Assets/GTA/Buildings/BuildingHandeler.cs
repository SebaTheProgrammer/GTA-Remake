using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using StarterAssets;
using UnityEditor;

public class BuildingHandeler : MonoBehaviour
{
    private Building[] m_AllBuildings;

    [SerializeField]
    private GameObject Ui;

    [Header("The BuildingMenu")]
    [SerializeField]
    private TMP_Text m_TextBox_Name;
    [SerializeField]
    private TMP_Text m_TextBox_AskingPrice;
    [SerializeField]
    private TMP_Text m_TextBox_StandardPrice;

    [SerializeField]
    private TMP_Text m_TextBox_Popularity;
    [SerializeField]
    private TMP_Text m_TextBox_Reputation;
    [SerializeField]
    private TMP_Text m_TextBox_DailyCash;

    private int m_BuildingIndex;

    private GameObject m_Player;
    void Start()
    {
        m_AllBuildings = Cash.Instance.ReturnBuilding();

        if (m_AllBuildings != null)
        {
            for (int index = 0; index < m_AllBuildings.Length; ++index)
            {
                m_AllBuildings[index].AskingPrice = m_AllBuildings[index].AskingPriceMin;
                m_AllBuildings[index].StandardPrice = m_AllBuildings[index].AskingPriceMin;
            }
        }
        Ui.SetActive(false);
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OpenBuildingMenu(int index)
    {
        m_BuildingIndex = index;

        Ui.SetActive(true);

        m_TextBox_Name.text = m_AllBuildings[m_BuildingIndex].Name;
        m_TextBox_Reputation.text = Reputation.Instance.GetRepu().ToString();

        m_TextBox_Popularity.text = m_AllBuildings[m_BuildingIndex].Popularity.ToString();
        m_TextBox_StandardPrice.text = m_AllBuildings[m_BuildingIndex].StandardPrice.ToString();
        m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

        m_TextBox_DailyCash.text=Cash.Instance.GetDailyCash().ToString();

        m_Player.GetComponent<ThirdPersonController>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseBuildingMenu()
    {
        Ui.SetActive(false);
        m_Player.GetComponent<ThirdPersonController>().enabled = true;


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpAskingPrice()
    {
        if (m_AllBuildings[m_BuildingIndex].Popularity <= 5)
        {
            if (Reputation.Instance.GetRepu() <= 25)
            {
                if (m_AllBuildings[m_BuildingIndex].AskingPrice + m_AllBuildings[m_BuildingIndex].AddCashValue <= m_AllBuildings[m_BuildingIndex].AskingPriceMax - 300)
                {
                    m_AllBuildings[m_BuildingIndex].AskingPrice += m_AllBuildings[m_BuildingIndex].AddCashValue;
                    m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

                    Cash.Instance.AddDailyCash(m_AllBuildings[m_BuildingIndex].AddCashValue);
                    m_TextBox_DailyCash.text = Cash.Instance.GetDailyCash().ToString();
                }
            }
            else
            {
                if (m_AllBuildings[m_BuildingIndex].AskingPrice + m_AllBuildings[m_BuildingIndex].AddCashValue <= m_AllBuildings[m_BuildingIndex].AskingPriceMax - 150)
                {
                    m_AllBuildings[m_BuildingIndex].AskingPrice += m_AllBuildings[m_BuildingIndex].AddCashValue;
                    m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

                    Cash.Instance.AddDailyCash(m_AllBuildings[m_BuildingIndex].AddCashValue);
                    m_TextBox_DailyCash.text = Cash.Instance.GetDailyCash().ToString();
                }
            }
        }
        else
        {
            if (Reputation.Instance.GetRepu() <= 25)
            {
                if (m_AllBuildings[m_BuildingIndex].AskingPrice + m_AllBuildings[m_BuildingIndex].AddCashValue <= m_AllBuildings[m_BuildingIndex].AskingPriceMax - 200)
                {
                    m_AllBuildings[m_BuildingIndex].AskingPrice += m_AllBuildings[m_BuildingIndex].AddCashValue;
                    m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

                    Cash.Instance.AddDailyCash(m_AllBuildings[m_BuildingIndex].AddCashValue);
                    m_TextBox_DailyCash.text = Cash.Instance.GetDailyCash().ToString();
                }
            }
            else
            {
                if (m_AllBuildings[m_BuildingIndex].AskingPrice + m_AllBuildings[m_BuildingIndex].AddCashValue <= m_AllBuildings[m_BuildingIndex].AskingPriceMax)
                {
                    m_AllBuildings[m_BuildingIndex].AskingPrice += m_AllBuildings[m_BuildingIndex].AddCashValue;
                    m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

                    Cash.Instance.AddDailyCash(m_AllBuildings[m_BuildingIndex].AddCashValue);
                    m_TextBox_DailyCash.text = Cash.Instance.GetDailyCash().ToString();
                }
            }
        }
    }

    public void DownAskingPrice()
    {
        if (m_AllBuildings[m_BuildingIndex].AskingPrice - m_AllBuildings[m_BuildingIndex].AddCashValue >= m_AllBuildings[m_BuildingIndex].AskingPriceMin)
        {
            m_AllBuildings[m_BuildingIndex].AskingPrice -= m_AllBuildings[m_BuildingIndex].AddCashValue;
            m_TextBox_AskingPrice.text = m_AllBuildings[m_BuildingIndex].AskingPrice.ToString();

            Cash.Instance.AbstractDailyCash(m_AllBuildings[m_BuildingIndex].AddCashValue);
            m_TextBox_DailyCash.text = Cash.Instance.GetDailyCash().ToString();
        }
    }
}
