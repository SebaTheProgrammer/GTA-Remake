using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thirst : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int priceOneDrink = 2;
    [SerializeField]
    private RectTransform ThirstBar;
    [SerializeField]
    private float thirst, Maxthirst;
    [SerializeField]
    private float loss;
    [SerializeField]
    private float thirstBuyMultipl;
    [SerializeField]
    private float StartDecayHp;
    [SerializeField]
    private int drinkIndex;
    [SerializeField]
    private float MaxSizeOfDrinks;

    [SerializeField]
    private GameObject m_Marker;

    [Header("Objects")]
    [SerializeField]
    private GameObject[] theObjects;
    [SerializeField]
    private GameObject[] theObjects2;

    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip drink;

    private float m_PercentUnit;
    bool m_HasBoughtShelves1 = false;
    bool m_HasBoughtShelves2 = false;


    // Start is called before the first frame update
    void Start()
    {
        m_PercentUnit = 1f / ThirstBar.anchorMax.x;
        src.clip = drink;

        for (int i = 0; i < theObjects.Length; i++)
        {
            theObjects[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < theObjects2.Length; i++)
        {
            theObjects2[i].gameObject.SetActive(false);
        }

        m_Marker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (thirst < 0.0f) thirst = 0.0f;
        else if (thirst > Maxthirst) thirst = Maxthirst;

        ThirstBar.anchorMax = new Vector2(thirst * m_PercentUnit / 100f, ThirstBar.anchorMax.y);

        thirst -= loss * Time.deltaTime;

        if (thirst < StartDecayHp)
        {
            HP.Instance.StartThirstDecay();
            m_Marker.SetActive(true);
        }
        else
        {
            HP.Instance.StopThirstDecay();
            m_Marker.SetActive(false);
        }

        //show drinks
        //1
        if (m_HasBoughtShelves1)
        {
            int temp = drinkIndex;
            if (drinkIndex > theObjects.Length)
            {
                temp = theObjects.Length;
            }
            for (int index = 0; index < temp; index++)
            {
                theObjects[index].gameObject.SetActive(true);
            }
            for (int index = temp; index < theObjects.Length; index++)
            {
                theObjects[index].gameObject.SetActive(false);
            }
        }

        //2
        if (m_HasBoughtShelves2)
        {
            int two = drinkIndex - theObjects.Length;

            if (two > theObjects2.Length)
            {
                two = theObjects2.Length;
            }
            if (two >= 0.0f)
            {
                for (int index = 0; index < two; index++)
                {
                    theObjects2[index].gameObject.SetActive(true);
                }
                for (int index = two; index < theObjects2.Length; index++)
                {
                    theObjects2[index].gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddThirst(int amount)
    {
        bool loop = true;
        if (Cash.Instance.GetCash() >= amount * priceOneDrink)
        {
            while (loop)
            {
                if ((thirst + amount * thirstBuyMultipl) <= Maxthirst + thirstBuyMultipl - 10)
                {
                    thirst += amount * thirstBuyMultipl;
                    Cash.Instance.AbstractCash(priceOneDrink * amount);
                    loop = false;
                }
                else
                {
                    if (amount >= 1 && m_HasBoughtShelves1)
                    {
                        if (drinkIndex <= theObjects.Length)
                        {
                            drinkIndex += 1;
                            amount -= 1;
                            Cash.Instance.AbstractCash(priceOneDrink);
                        }
                        else
                        {
                            if (drinkIndex - theObjects.Length <= theObjects2.Length && m_HasBoughtShelves2)
                            {
                                drinkIndex += 1;
                                amount -= 1;
                                Cash.Instance.AbstractCash(priceOneDrink);
                            }
                            else
                            {
                                drinkIndex = theObjects.Length;
                                loop = false;
                            }
                        }
                    }
                    else
                    {
                        thirst += amount * thirstBuyMultipl;
                        Cash.Instance.AbstractCash(priceOneDrink);
                        loop = false;
                    }
                }
            }

            src.Play();
        }
    }

    public void Drink()
    {
        if (drinkIndex > 0)
        {
            drinkIndex -= 1;
            src.Play();
            thirst += thirstBuyMultipl;
        }
    }

    public void HasBoughtShelves()
    {
        m_HasBoughtShelves1 = true;
    }

    public void HasBoughtShelves2()
    {
        m_HasBoughtShelves2 = true;
    }
}
