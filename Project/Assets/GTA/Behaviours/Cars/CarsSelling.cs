using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsSelling : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SellUi;

    [SerializeField]
    private int[] m_ValuesCar;

    [SerializeField]
    private GameObject m_DailyLimitUi;

    [SerializeField]
    private GameObject m_NoCarUi;

    [SerializeField]
    private string m_SellButton;

    private bool m_HasCarToSell;
    [SerializeField]
    private int m_HowMuchCarsCanBeSoldToday;
    private int m_CarsSoldToday;

    private bool m_CanUseAll;
    private bool m_IsClose;

    // Start is called before the first frame update
    void Start()
    {
        m_SellUi?.SetActive(false);
        m_DailyLimitUi?.SetActive(false);
        m_NoCarUi?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CanUseAll)
        {
            if (m_IsClose)
            {
                if (Input.GetKeyUp(m_SellButton))
                {
                    if (m_HasCarToSell)
                    {
                        if (m_CarsSoldToday < m_HowMuchCarsCanBeSoldToday)
                        {
                            m_CarsSoldToday += 1;
                            int cash = m_ValuesCar[Random.Range(0, m_ValuesCar.Length)];
                            Cash.Instance.AddCash(cash);
                            m_HasCarToSell = false;
                        }
                    }
                    else
                    {
                        if (m_CarsSoldToday < m_HowMuchCarsCanBeSoldToday)
                        {
                            m_NoCarUi?.SetActive(true);
                        }
                        else
                        {
                            m_DailyLimitUi?.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public void HasBoughtCarShop()
    {
        m_CanUseAll = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (m_CanUseAll)
        {
            if (col.gameObject.tag == "Player")
            {
                if (m_HasCarToSell)
                {
                    m_SellUi?.SetActive(true);
                }
                else
                {
                    if (m_CarsSoldToday < m_HowMuchCarsCanBeSoldToday)
                    {
                        m_NoCarUi?.SetActive(true);
                    }
                    else
                    {
                        m_DailyLimitUi?.SetActive(true);
                    }
                }
                m_IsClose = true;
            }

            if (col.gameObject.tag == "Cars")
            {
                m_HasCarToSell = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (m_CanUseAll)
        {
            if (col.gameObject.tag == "Player")
            {
                if (m_SellUi.activeSelf)
                {
                    m_SellUi?.SetActive(false);
                }
                if (m_DailyLimitUi.activeSelf)
                {
                    m_DailyLimitUi?.SetActive(false);
                }
                if (m_NoCarUi.activeSelf)
                {
                    m_NoCarUi?.SetActive(false);
                }
                m_IsClose = false;
            }
            if (col.gameObject.tag == "Cars")
            {
                m_HasCarToSell = false;
            }
        }
    }
}
