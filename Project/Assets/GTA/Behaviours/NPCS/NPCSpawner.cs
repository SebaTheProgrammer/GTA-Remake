using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] arrayOfNPCS;
   
    void Awake()
    {
        //als u zich afvraagt waarom ik geen instances spawn van die npcs,
        //is omdat als die hun patroling hebben anders kijken naar hun 'parent' en hierdoor nooit
        // veranderen van patrol point omdat ze nooit dicht genoeg komen
        //rare bug, dus lelijk moeten oplossen...
    }
}
