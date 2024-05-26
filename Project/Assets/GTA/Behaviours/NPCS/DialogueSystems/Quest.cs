using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Quest : MonoBehaviour
{
    [Header("QUEST")]
    [SerializeField]
    private string m_ObjectiveQuest;
    private int m_SizeQuest;
    private int m_CountDoneQuest;

    [SerializeField]
    private int m_CashReward;
    [SerializeField]
    private int m_ReputationReward;

    private float m_TimerCompletedQuest;
    private float m_TimeCompletedQuestAppear=1.5f;

    [Header("---")]
    [SerializeField]
    private bool m_KillQuest;
    [SerializeField]
    private GameObject[] m_WichNPCToKill;
    private bool[] m_WichNPCAlreadyKilled;
    [Header("---")]
    [SerializeField]
    private bool m_SearchQuest;
    [SerializeField]
    private int m_DistanceTo;
    [SerializeField]
    private bool m_HideWhenFound;
    [SerializeField]
    private GameObject[] m_WichPlaceToSearch;
    private bool[] m_WichPlaceAlreadySearched;

    [Header("---")]
    [SerializeField]
    private bool m_TalkQuest;
    [SerializeField] 
    private ChatsNPC m_WichNPCToTalk;

    [Header("BEHAVIORS")]
    [SerializeField]
    private bool m_Neutral;
    [SerializeField]
    private bool m_Angry;
    [SerializeField]
    private bool m_Scared;

    [Header("UI")]
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private GameObject m_QuestUI;
    [SerializeField]
    private TMP_Text m_ObjectiveText;
    [SerializeField]
    private TMP_Text m_DoneText;
    [SerializeField]
    private TMP_Text m_CountDoneText;
    [SerializeField]
    private TMP_Text m_CashRewardText;
    [SerializeField]
    private TMP_Text m_RepuRewardText;
    [SerializeField]
    private GameObject m_Marker;

    private bool m_HasStartedQuest;
    private bool m_HasCompletedThisQuest;
    private bool m_StopAll;
    private GameObject m_Player;
    private GameObject m_MainCamera;

    private NPCSHp m_NPCSHp;

    private bool m_HasPrevQuest;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NPCSHp = this.GetComponent<NPCSHp>();

        m_Marker?.SetActive(false);

        Reset();

        if (m_KillQuest)
        {
            m_SizeQuest = m_WichNPCToKill.Length;
            m_WichNPCAlreadyKilled = new bool[m_WichNPCToKill.Length];

            for (int index = 0; index < m_WichNPCToKill.Length; ++index)
            {
                m_WichNPCAlreadyKilled[index] = false;
            }
            m_SizeQuest = m_WichNPCToKill.Length;
        }
        else if (m_SearchQuest)
        {
            m_SizeQuest = m_WichPlaceToSearch.Length;
            m_WichPlaceAlreadySearched = new bool[m_WichPlaceToSearch.Length];

            for (int index = 0; index < m_WichPlaceToSearch.Length; ++index)
            {
                m_WichPlaceAlreadySearched[index] = false;
            }
        }
        else if (m_TalkQuest)
        {
            m_SizeQuest = 1;
        }
    }

    private void Reset()
    {
        m_ObjectiveText.text = "No active quest";
        m_DoneText.text = "0";
        m_CountDoneText.text = "0";
        m_CashRewardText.text = ("/ $");
        m_RepuRewardText.text = ("/ rp");
        m_QuestUI?.SetActive(false);
        m_Marker?.SetActive(false);
        m_NPCSHp?.SetNoRespawn();
    }

    void Update()
    {
        if (!m_StopAll)
        {
            if (!m_NPCSHp.IsAlive())
            {
                QuestsInstance.Instance.EndQuest();
                Reset();
                m_StopAll = true;
            }
            else
            {
                if (!QuestsInstance.Instance.HasActiveQuest())
                {
                    if (!m_HasStartedQuest && !m_HasCompletedThisQuest && !m_HasPrevQuest)
                    {
                        m_Marker?.SetActive(true);
                        m_Marker.transform.rotation = m_MainCamera.transform.rotation;
                    }
                    else
                    {
                        m_Marker?.SetActive(false);
                    }
                }
                else
                {
                    m_Marker?.SetActive(false);
                }


                if (m_HasStartedQuest && !m_HasCompletedThisQuest)
                {
                    if (m_KillQuest)
                    {
                        FightQuest();
                    }

                    if (m_SearchQuest)
                    {
                        SearchQuest();
                    }

                    if (m_TalkQuest)
                    {
                        TalkQuest();
                    }

                    if (m_CountDoneQuest >= m_SizeQuest)
                    {
                        m_HasCompletedThisQuest = true;
                        m_AudioSource.Play();
                    }

                    StartQuest();
                }

                if (m_HasCompletedThisQuest)
                {
                    CompletedQuest();
                }
            }
        }
    }
    public void StartQuest()
    {
        if (!m_HasCompletedThisQuest)
        {
            m_ObjectiveText.text = m_ObjectiveQuest;
            m_DoneText.text = m_SizeQuest.ToString();
            m_CashRewardText.text = (m_CashReward + "$");
            m_RepuRewardText.text = (m_ReputationReward + "rp");
            m_QuestUI?.SetActive(true);
            m_HasStartedQuest = true;
        }
    }
    private void CompletedQuest()
    {
        if (m_HasCompletedThisQuest)
        {
            m_ObjectiveText.text = "COMPLETED";

            m_TimerCompletedQuest += Time.deltaTime;

            if (m_TimerCompletedQuest >= m_TimeCompletedQuestAppear)
            {
                Cash.Instance.AddCash(m_CashReward);
                Reputation.Instance.Add(m_ReputationReward);

                QuestsInstance.Instance.EndQuest();
                m_StopAll = true;
                Reset();
            }
        }
    }

    //different quests
    private void FightQuest()
    {
        for (int index = 0; index < m_SizeQuest; ++index)
        {
            if (!m_WichNPCAlreadyKilled[index])
            {
                m_WichNPCToKill[index].GetComponent<ChatsNPC>()?.ShowHuntedMarker();
            }
            if (!m_WichNPCToKill[index].GetComponent<NPCSHp>().IsAlive() && !m_WichNPCAlreadyKilled[index])
            {
                StartQuest();
                m_WichNPCAlreadyKilled[index] = true;
                m_CountDoneQuest += 1;
                m_CountDoneText.text = m_CountDoneQuest.ToString();
                m_WichNPCToKill[index].GetComponent<ChatsNPC>()?.HideMarker();
                StartQuest();
            }
        }
    }
    private void SearchQuest()
    {
        for (int index = 0; index < m_SizeQuest; ++index)
        {
            if (!m_WichPlaceAlreadySearched[index])
            {
                float distance = Vector3.Distance(m_Player.transform.position, m_WichPlaceToSearch[index].transform.position);
                if (distance <= m_DistanceTo)
                {
                    m_WichPlaceAlreadySearched[index] = true;
                    m_CountDoneQuest += 1;
                    m_CountDoneText.text = m_CountDoneQuest.ToString();

                    if (m_HideWhenFound)
                    {
                        m_WichPlaceToSearch[index]?.SetActive(false);
                    }
                }
            }
        }
    }
    private void TalkQuest()
    {
        if (m_WichNPCToTalk.gameObject.GetComponent<NPCSHp>().IsAlive())
        {
            m_WichNPCToTalk.ShowMarker();

            for (int index = 0; index < m_SizeQuest; ++index)
            {
                if (m_WichNPCToTalk.HasTalked())
                {
                    m_WichNPCToTalk.HideMarker();
                    m_CountDoneQuest += 1;
                    m_CountDoneText.text = m_CountDoneQuest.ToString();
                }
            }
        }
        else
        {
            m_StopAll = true;
        }
    }

    //bools
    public bool IsNeutral()
    {
        return m_Neutral;
    }
    public bool IsAngry()
    {
        return m_Angry;
    }
    public bool IsScared()
    {
        return m_Scared;
    }
    public bool HasDoneQuest()
    {
        return m_HasCompletedThisQuest;
    }
    public bool HasStartedQuest()
    {
        return m_HasStartedQuest;
    }
    public void HasPrevQuest(bool index)
    {
        m_HasPrevQuest = index;
    }
}
