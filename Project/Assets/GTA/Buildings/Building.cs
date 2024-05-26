using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building file", menuName = "Building files Archive")]

public class Building : ScriptableObject
{
    [Header("Stats")]
    public string Name;

    public float Popularity;

    public float DailyPopularity;

    public int AskingPriceMin;
    public int AskingPriceMax;
    public int AskingPrice;

    public int StandardPrice;
    public int AddCashValue;

    public bool IsAppartement;
}
