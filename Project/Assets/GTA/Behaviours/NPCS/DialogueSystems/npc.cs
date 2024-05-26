using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC files Archive")]

public class NPC : ScriptableObject
{
    //[SerializeField]
    //private List<NPC> managers = new List<NPC>();

    public string nameNpc;

    [Header("BASICS")]
    [TextArea(3, 15)]
    public string[] dialogue;
    [TextArea(3, 15)]
    public string[] playerDialogue;

    [Header("QUESTS")]
    public bool NoQuest;
}
