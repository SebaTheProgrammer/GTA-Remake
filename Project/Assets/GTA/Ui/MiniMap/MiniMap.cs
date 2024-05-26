using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private string m_MapButton;

    [Header("Ui")]
    [SerializeField]
    private GameObject m_mapUI;
    [SerializeField]
    private GameObject m_cashUI;
    [SerializeField]
    private GameObject m_DailycashUI;
    [SerializeField]
    private TMP_Text m_DailyCashText;
    [SerializeField]
    private GameObject m_timeUI;
    [SerializeField]
    private GameObject m_repuUI;
    [SerializeField]
    private GameObject m_playerUI;
    [SerializeField]
    private GameObject m_carUI;
    [SerializeField]
    private GameObject m_QuestUI;

    private bool m_IsOn;

    // Start is called before the first frame update
    void Start()
    {
        m_mapUI.SetActive(false);
        m_repuUI.SetActive(false);
        m_carUI.SetActive(false);
        m_QuestUI.SetActive(false);
        m_DailycashUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_MapButton))
        {
            if (!m_IsOn)
            {
                ShowAll();
            }
            else
            {
                HideAll();
            }
        }

        if (m_IsOn)
        {
            if (!Input.GetKeyDown(m_MapButton))
            {
                if (Input.anyKey)
                {
                    m_mapUI.GetComponent<CanvasGroup>().alpha = 0.3f;
                }
                else
                {
                    m_mapUI.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
            else
            {
                m_mapUI.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }

    public void HideAll()
    {
        m_mapUI.SetActive(false);
        m_cashUI.SetActive(false);
        m_timeUI.SetActive(false);
        m_repuUI.SetActive(false);
        m_DailycashUI.SetActive(false);

        if (QuestsInstance.Instance.HasActiveQuest())
        {
            m_QuestUI.SetActive(true);
        }
        m_IsOn = false;
    }

    private void ShowAll()
    {
        m_mapUI.SetActive(true);
        m_cashUI.SetActive(true);
        m_timeUI.SetActive(true);
        m_DailycashUI.SetActive(true);
        m_DailyCashText.text = "$"+Cash.Instance.GetDailyCash().ToString();
        m_QuestUI.SetActive(false);

        if (!Stars.Instance.HasStars())
        {
            m_repuUI.SetActive(true);
        }
        m_IsOn = true;
    }
}
