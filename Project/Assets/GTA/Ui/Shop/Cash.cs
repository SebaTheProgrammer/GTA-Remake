using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Cash : MonoBehaviour
{
    public static Cash Instance;

    [SerializeField]
    private int cash;
    [SerializeField]
    private int m_DailyCash;

    [SerializeField]
    private TMP_Text cashText;

    [SerializeField]
    private GameObject MoneyUi;
    private bool m_ShowedMoney;
    private float m_TotalTime;
    private float m_TotalShowedTimeMoney = 2f;

    [Header("Buildings")]
    [SerializeField]
    private Building[] m_AllBuildings;

    [SerializeField]
    private EnterBuilding m_Bar;
    [SerializeField]
    private GameObject m_BarIcon;
    [SerializeField]
    private GameObject m_BarMap;
    private bool m_HasBoughtBar;

    [SerializeField]
    private EnterBuilding m_GasStation;
    [SerializeField]
    private GameObject m_GasStationIcon;
    [SerializeField]
    private GameObject m_GasStationMap;
    private bool m_HasBoughtGasStation;

    [SerializeField]
    private EnterBuilding m_App1;
    [SerializeField]
    private GameObject m_App1Icon;
    [SerializeField]
    private GameObject m_App1Map;
    private bool m_HasBoughtApp1;

    [SerializeField]
    private EnterBuilding m_AutoService;
    [SerializeField]
    private CarsSelling m_CarsSelling;
    [SerializeField]
    private GameObject m_AutoServiceIcon;
    [SerializeField]
    private GameObject m_AutoServiceMap;
    private bool m_HasBoughtAutoService;

    [SerializeField]
    private EnterBuilding m_App2;
    [SerializeField]
    private GameObject m_App2Icon;
    [SerializeField]
    private GameObject m_App2Map;
    private bool m_HasBoughtApp2;

    [SerializeField]
    private Sleep m_Villa;
    [SerializeField]
    private GameObject m_VillaIcon;
    [SerializeField]
    private GameObject m_VillaMap;
    private bool m_HasBoughtVilla;

    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip buy;

    private bool m_HasBoughtPhone;

    void Start()
    {
        src.clip = buy;

        m_Bar.enabled = false;
        m_GasStation.enabled = false;
        m_App1.enabled = false;
        m_AutoService.enabled = false;
        m_App2.enabled = false;
        m_Villa.enabled = false;
        m_CarsSelling.enabled = false;
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCash(cash);

        if (m_ShowedMoney)
        {
            m_TotalTime += Time.deltaTime;
        }
        if (m_TotalTime > m_TotalShowedTimeMoney)
        {
            m_ShowedMoney = false;
            MoneyUi.gameObject?.SetActive(false);
            m_TotalTime = 0;
        }
    }

    void DisplayCash( float cashToDisplay)
    {
       cashText.text = "$"+cashToDisplay.ToString();
    }

    public int GetCash()
    {
        return cash;
    }
    public int GetDailyCash()
    {
        return m_DailyCash;
    }

    public void AddCash(int amount)
    {
        cash += amount;
        m_ShowedMoney = true;
        m_TotalTime = 0;
        MoneyUi.gameObject?.SetActive(true);
        src.pitch = Random.Range(1f, 1.3f);
        src.Play();
    }

    private void Buys(int amount)
    {
        src.pitch = Random.Range(1f, 1.3f);
        src.Play();
        cash -= amount;
        m_ShowedMoney = true;
        m_TotalTime = 0;
        MoneyUi.gameObject?.SetActive(true);
    }

    public void AbstractCash(int amount)
    {
        if (cash - amount >= 0)
        {
            Buys(amount);
        }
    }

    public void AddDailyCash(int amount)
    {
        m_DailyCash += amount;
    }

    public void AbstractDailyCash(int amount)
    {
        m_DailyCash -= amount;
    }

    public void BuyPhone(int amount)
    {
        if (!m_HasBoughtPhone)
        {
            if (cash - amount >= 0)
            {
                m_HasBoughtPhone = true;
                Buys(amount);
            }
        }
    }
    public bool HasBoughtPhone()
    {
        return m_HasBoughtPhone;
    }

    public void AddReputationBuildings()
    {
        if (Reputation.Instance.GetRepu() > Reputation.Instance.GetMinForPopu())
        {
            if (m_HasBoughtBar)
            {
                m_AllBuildings[0].Popularity += m_AllBuildings[0].DailyPopularity;
            }
            if (m_HasBoughtGasStation)
            {
                m_AllBuildings[1].Popularity += m_AllBuildings[1].DailyPopularity;
            }
            if (m_HasBoughtApp1)
            {
                m_AllBuildings[2].Popularity += m_AllBuildings[2].DailyPopularity;
            }
            if (m_HasBoughtAutoService)
            {
                m_AllBuildings[3].Popularity += m_AllBuildings[3].DailyPopularity;
            }
            if (m_HasBoughtApp2)
            {
                m_AllBuildings[4].Popularity += m_AllBuildings[4].DailyPopularity;
            }
        }
        else
        {
            if (m_HasBoughtBar)
            {
                if (m_AllBuildings[0].Popularity - m_AllBuildings[0].DailyPopularity >= 0)
                {
                    m_AllBuildings[0].Popularity -= m_AllBuildings[0].DailyPopularity;
                }
            }
            if (m_HasBoughtGasStation)
            {
                if (m_AllBuildings[1].Popularity - m_AllBuildings[1].DailyPopularity >= 0)
                {
                    m_AllBuildings[1].Popularity -= m_AllBuildings[1].DailyPopularity;
                }
            }

            if (m_HasBoughtApp1)
            {
                if (m_AllBuildings[2].Popularity - m_AllBuildings[2].DailyPopularity >= 0)
                {
                    m_AllBuildings[2].Popularity -= m_AllBuildings[2].DailyPopularity;
                }
            }
            if (m_HasBoughtAutoService)
            {
                if (m_AllBuildings[3].Popularity - m_AllBuildings[3].DailyPopularity >= 0)
                {
                    m_AllBuildings[3].Popularity -= m_AllBuildings[3].DailyPopularity;
                }
            }
            if (m_HasBoughtApp2)
            {
                if (m_AllBuildings[4].Popularity - m_AllBuildings[4].DailyPopularity >= 0)
                {
                    m_AllBuildings[4].Popularity -= m_AllBuildings[4].DailyPopularity;
                }
            }

        }
    }


    //All Buildings
    public Building[] ReturnBuilding()
    {
        return m_AllBuildings;
    }
    public bool HasBoughtBuilding(int index)
    {
        if (index == 0)
        {
            return m_HasBoughtBar;
        }
        if (index == 1)
        {
            return m_HasBoughtGasStation;
        }
        if (index == 2)
        {
            return m_HasBoughtApp1;
        }
        if (index == 3)
        {
            return m_HasBoughtAutoService;
        }
        if (index == 4)
        {
            return m_HasBoughtApp2;
        }
        if (index == 5)
        {
            return m_HasBoughtVilla;
        }
        else
        {
            return false;
        }

    }
    public void BoughtBar(int amount)
    {
        if (!m_HasBoughtBar)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);
                m_DailyCash += m_AllBuildings[0].AskingPriceMin;
                m_Bar.enabled = true;
                m_BarIcon?.SetActive(true);
                m_BarMap?.SetActive(true);
                m_HasBoughtBar = true;
            }
        }
    }
    public void BoughtGasStation(int amount)
    {
        if (!m_HasBoughtGasStation)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);

                m_GasStation.enabled = true;
                m_DailyCash += m_AllBuildings[1].AskingPriceMin;
                m_GasStationIcon?.SetActive(true);
                m_GasStationMap?.SetActive(true);
                m_HasBoughtGasStation = true;
            }
        }
    }
    public void BoughtApp1(int amount)
    {
        if (!m_HasBoughtApp1)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);

                m_App1.enabled = true;
                m_DailyCash += m_AllBuildings[2].AskingPriceMin;
                m_App1Icon?.SetActive(true);
                m_App1Map?.SetActive(true);
                m_HasBoughtApp1 = true;
            }
        }
    }
    public void BoughtCarService(int amount)
    {
        if (!m_HasBoughtAutoService)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);

                m_AutoService.enabled = true;
                m_DailyCash += m_AllBuildings[3].AskingPriceMin;
                m_AutoServiceIcon?.SetActive(true);
                m_AutoServiceMap?.SetActive(true);
                m_HasBoughtAutoService=true;
                m_CarsSelling.enabled = true;
                m_CarsSelling?.HasBoughtCarShop();
            }
        }
    }
    public void BoughtApp2(int amount)
    {
        if (!m_HasBoughtApp2)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);

                m_App2.enabled = true;
                m_DailyCash += m_AllBuildings[4].AskingPriceMin;
                m_App2Icon?.SetActive(true);
                m_App2Map?.SetActive(true);
                m_HasBoughtApp2 = true;
            }
        }
    }
    public void BoughtVilla(int amount)
    {
        if (!m_HasBoughtVilla)
        {
            if (cash - amount >= 0)
            {
                Buys(amount);

                m_Villa.enabled = true;
                m_DailyCash += m_AllBuildings[5].AskingPriceMin;
                m_VillaIcon?.SetActive(true);
                m_VillaMap?.SetActive(true);
                m_HasBoughtVilla = true;
            }
        }
    }
}
