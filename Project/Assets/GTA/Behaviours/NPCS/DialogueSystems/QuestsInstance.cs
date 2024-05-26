using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsInstance : MonoBehaviour
{
    public static QuestsInstance Instance;
    private bool m_HasQuest;

    void Awake()
    {
        Instance = this;
    }

    public bool HasActiveQuest()
    {
        return m_HasQuest;
    }

    public void StartQuest()
    {
        m_HasQuest = true;
    }

    public void EndQuest()
    {
        m_HasQuest = false;
    }
}
