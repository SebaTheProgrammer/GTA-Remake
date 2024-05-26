using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichHood : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DontEnterUi;

    [SerializeField]
    private ChatsNPC[] m_ChatNPCS;

    [SerializeField]
    private Collider m_Collider;
    [SerializeField]
    private int m_RepuBLock;

    private bool m_CanEnter;

    void Start()
    {
        m_DontEnterUi?.SetActive(false);
        m_Collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CanEnter)
        {
            for (int index = 0; index < m_ChatNPCS.Length; ++index)
            {
                if (m_ChatNPCS[index].HasTalked()||Reputation.Instance.GetRepu()< m_RepuBLock)
                {
                    m_Collider.enabled = false;
                    m_CanEnter = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !m_CanEnter)
        {
            m_DontEnterUi?.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(m_DontEnterUi.activeSelf)
            {
                m_DontEnterUi?.SetActive(false);
            }
        }
    }
}
