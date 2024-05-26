using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ChatsNPC : MonoBehaviour
{
    [SerializeField]
    private string AcceptButton;

    [SerializeField]
    private DialogueManger dialogueManger;

    [SerializeField]
    private GameObject canTalkUI;

    [SerializeField]
    private int[] indexNPC;

    private bool canTalk;
    private bool isTalking;

    [SerializeField]
    private Quest[] m_Quest;
    private int m_IndexWichQuest;

    [SerializeField]
    private bool m_HasPrevQuest;
    [SerializeField]
    private Quest[] m_PrevQuest;

    private bool m_HasTalked;

    [SerializeField]
    private GameObject m_QuestionMarker;

    private bool m_ShowMarker;

    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        canTalkUI?.SetActive(false);
        m_QuestionMarker?.SetActive(false);

        m_IndexWichQuest = 0;

        if (m_HasPrevQuest)
        {
            m_Quest[m_IndexWichQuest].HasPrevQuest(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(AcceptButton) && canTalk && !isTalking && this.GetComponent<NPCSHp>().IsAlive())
        {
            if (!m_HasPrevQuest)
            {
                canTalkUI?.SetActive(false);
                dialogueManger.StartConvo(indexNPC[m_IndexWichQuest], this.GetComponent<NPCSHp>(), this.GetComponent<NpcBehaviour>(), m_Quest[m_IndexWichQuest]);
                isTalking = true;

                m_HasTalked = true;
            }
        }

        if (m_ShowMarker)
        {
            m_QuestionMarker.transform.rotation = mainCamera.transform.rotation;
        }

        if (m_HasPrevQuest)
        {
            for (int index = 0; index < m_PrevQuest.Length; ++index)
            {
                if (m_PrevQuest[index].HasStartedQuest())
                {
                    m_Quest[0].HasPrevQuest(false);
                    m_HasPrevQuest = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && this.GetComponent<NPCSHp>().IsAlive() && !this.GetComponent<NPCSHp>().FollowPlayer() && !m_Quest[m_IndexWichQuest].HasDoneQuest())
        {
            if (!canTalk)
            {
                if (!m_HasPrevQuest)
                {
                    canTalk = true;
                    canTalkUI?.SetActive(true);
                }
                else
                {
                    canTalk = false;
                    canTalkUI?.SetActive(false);
                }
            }
        }
        else if (m_Quest[m_IndexWichQuest].HasDoneQuest())
        {
            if (m_IndexWichQuest + 1 < m_Quest.Length)
            {
                m_IndexWichQuest += 1;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (canTalk)
            {
                canTalkUI.SetActive(false);
                canTalk = false;

                if (isTalking)
                {
                    dialogueManger.EndConvo();
                    isTalking = false;
                }
            }
        }
    }

    public bool HasTalked()
    {
        return m_HasTalked;
    }

    public void ShowMarker()
    {
        if (!m_HasPrevQuest)
        {
            m_QuestionMarker?.SetActive(true);
            m_ShowMarker = true;
        }
        else
        {
            m_QuestionMarker?.SetActive(false);
            m_ShowMarker = false;
        }
    }
    public void ShowHuntedMarker()
    {
        m_QuestionMarker?.SetActive(true);
        m_ShowMarker = true;
    }
    public void HideMarker()
    {
        m_QuestionMarker?.SetActive(false);
        m_ShowMarker = false;
    }
}
