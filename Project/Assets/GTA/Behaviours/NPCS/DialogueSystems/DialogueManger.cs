using UnityEngine;
using TMPro;
using StarterAssets;

public class DialogueManger : MonoBehaviour
{
    [SerializeField]
    private NPC[] npcs;

    [Header("The NPC")]
    [SerializeField]
    private TMP_Text npcName;
    [SerializeField]
    private TMP_Text npcDialogueBox;

    [SerializeField]
    private TMP_Text playerResponse1;
    [SerializeField]
    private string emptyString;

    [Header("Buttons")]
    [SerializeField]
    private string AcceptButton;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject dialogueUI;

    private bool m_IsTalking = false;
    private int m_CurResponseTracker = 0;
    private int m_WichNPC;

    private NPCSHp m_NPCSHp;
    private NpcBehaviour m_NPCBehaviour;
    private Quest m_NPCQuest;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI?.SetActive(false);
    }

    void Update()
    {
        if (m_IsTalking)
        {
            for (int index = 0; index <= npcs[m_WichNPC].dialogue.Length; ++index)
            {
                if (m_CurResponseTracker == index)
                {
                    if (Input.GetKeyDown(AcceptButton))
                    {
                        if (index >= npcs[m_WichNPC].playerDialogue.Length)
                        {
                            if (npcs[m_WichNPC].NoQuest)
                            { 

                            }
                            else
                            {
                                //Behavior
                                if (!QuestsInstance.Instance.HasActiveQuest()&& !m_NPCQuest.HasDoneQuest())
                                {
                                    QuestsInstance.Instance.StartQuest();

                                    m_NPCQuest?.StartQuest();
                                    m_NPCSHp?.SetNoRespawn();

                                    if (m_NPCQuest.IsAngry())
                                    {
                                        m_NPCSHp?.GetQuestAngry();
                                        m_NPCBehaviour?.ChangeAttackOrRun(0);
                                    }
                                    else if (m_NPCQuest.IsScared())
                                    {
                                        m_NPCSHp?.GetQuestAngry();
                                        m_NPCBehaviour?.ChangeAttackOrRun(1);
                                    }
                                    else
                                    {
                                        //neutral
                                    }
                                }
                            }

                            EndConvo();
                        }
                        else
                        {
                            playerResponse1.text = npcs[m_WichNPC].playerDialogue[index];
                            npcDialogueBox.text = npcs[m_WichNPC].dialogue[index];  
                        }
                    }
                }
            }

            if (Input.GetKeyUp(AcceptButton))
            {
                m_CurResponseTracker += 1;
            }
        }
    }

    public void StartConvo(int index, NPCSHp NPCSHp, NpcBehaviour NPCBehaviour, Quest NPCQuest)
    {
        m_WichNPC = index;
        m_NPCSHp = NPCSHp;
        m_NPCBehaviour = NPCBehaviour;

        if (!npcs[m_WichNPC].NoQuest)
        {
            m_NPCQuest = NPCQuest;
        }

        dialogueUI?.SetActive(true);

        m_CurResponseTracker = 0;
        npcName.text = npcs[m_WichNPC].nameNpc;
        npcDialogueBox.text = npcs[m_WichNPC].dialogue[0];

        playerResponse1.text = emptyString;

        int tempLength = npcs[m_WichNPC].playerDialogue.Length;

        if (tempLength >= 1)
        {
            playerResponse1.text = npcs[m_WichNPC].playerDialogue[0];
        }

        m_IsTalking = true;

        player.GetComponent<ThirdPersonController>().enabled = false;

    }

    public void EndConvo()
    {
        m_IsTalking = false;
        dialogueUI?.SetActive(false);

        player.GetComponent<ThirdPersonController>().enabled = true;
    }
}
