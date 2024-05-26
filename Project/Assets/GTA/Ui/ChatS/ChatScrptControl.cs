using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScrptControl : MonoBehaviour
{
    //chat
    [SerializeField]
    private ChatScript chat1;

    [SerializeField]
    private float amountMoneyFirstChat=5;

    private bool C2=true;
    [SerializeField]
    private ChatScript chat2;

    private bool CMilion = true;
    [SerializeField]
    private ChatScript chatM;



   // Start is called before the first frame update
   void Start()
    {
        chat1?.Show();
        chat1?.StartDialogue();

        chat2?.Hide();

        chatM?.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestsInstance.Instance.HasActiveQuest())
        {
            C2 = false;
        }
        if (Cash.Instance.GetCash() >= amountMoneyFirstChat && C2)
        {
            C2 = false;
            chat2?.Show();
            chat2?.StartDialogue();
        }

        if (Cash.Instance.GetCash() >= 100000 && CMilion)
        {
            CMilion = false;
            chatM?.Show();
            chatM?.StartDialogue();
        }

        //if(Hunger.Instance.Hunger or thirst <= 20){start warning}
    }
}
