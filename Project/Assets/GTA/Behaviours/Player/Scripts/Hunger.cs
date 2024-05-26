using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int m_PriceOneFood = 3;
    [SerializeField]
    private RectTransform HungerBar;
    [SerializeField]
    private float hunger, MaxHunger;
    [SerializeField]
    private float loss;
    [SerializeField]
    private float thirstBuyMultipl;
    [SerializeField]
    private float StartDecayHp;
    [SerializeField]
    private int foodIndex;
    [SerializeField]
    private float MaxSizeOfFood;

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
    private AudioClip eat;

    private float m_PercentUnit;

    bool m_HasBoughtShelves1 = false;
    bool m_HasBoughtShelves2 = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PercentUnit = 1f / HungerBar.anchorMax.x;
        src.clip = eat;

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
        if (hunger < 0.0f) hunger = 0.0f;
        else if (hunger > MaxHunger) hunger = MaxHunger;

        HungerBar.anchorMax = new Vector2(hunger * m_PercentUnit / 100f, HungerBar.anchorMax.y);

        hunger -= loss * Time.deltaTime;

        if (hunger < StartDecayHp)
        {
            HP.Instance.StartHungerDecay();
            m_Marker.SetActive(true);
        }
        else
        {
           HP.Instance.StopHungerDecay();
            m_Marker.SetActive(false);
        }


        //show foods
        //1
        if (m_HasBoughtShelves1)
        {
            int temp = foodIndex;
            if (foodIndex > theObjects.Length)
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
            int two = foodIndex - theObjects.Length;

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

    public void AddFood(int amount)
    {
        bool loop = true;
        if (Cash.Instance.GetCash() >= amount * m_PriceOneFood)
        {
            while (loop)
            {
                if ((hunger + amount * thirstBuyMultipl) <= MaxHunger + thirstBuyMultipl- 10)
                {
                    hunger += amount * thirstBuyMultipl;
                    Cash.Instance.AbstractCash(m_PriceOneFood* amount);
                    loop = false;
                }
                else
                {
                    if (amount >= 1 && m_HasBoughtShelves1)
                    {
                        if (foodIndex <= theObjects.Length)
                        {
                            foodIndex += 1;
                            amount -= 1;
                            Cash.Instance.AbstractCash(m_PriceOneFood);
                        }
                        else
                        {
                            if (foodIndex - theObjects.Length <= theObjects2.Length && m_HasBoughtShelves2)
                            {
                                foodIndex += 1;
                                amount -= 1;
                                Cash.Instance.AbstractCash(m_PriceOneFood);
                            }
                            else
                            {
                                foodIndex = theObjects.Length;
                                loop = false;
                            }
                        }
                    }
                    else
                    {
                        hunger += amount * thirstBuyMultipl;
                        Cash.Instance.AbstractCash(m_PriceOneFood);
                        loop = false;
                    }
                }
            }

            src.Play();
        }
    }

    public void Eat()
    {
        if (foodIndex > 0)
        {
            foodIndex -= 1;
            src.Play();
            hunger += thirstBuyMultipl;
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
