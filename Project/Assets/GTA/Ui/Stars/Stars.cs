using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public static Stars Instance;

    public int stars = 0;

    private bool m_HasPolice;

    [SerializeField]
    private AudioSource src;

    [SerializeField]
    private AudioClip policeSong;

    [SerializeField]
    int m_TimeWaitPoliceGone;

    [SerializeField]
    private int m_MaxStars;

    [SerializeField]
    private GameObject star1red;
    [SerializeField]
    private GameObject star1blue;
    [SerializeField]
    private bool light1on;

    [SerializeField]
    private GameObject star2red;
    [SerializeField]
    private GameObject star2blue;
    [SerializeField]
    private bool light2on;

    [SerializeField]
    private GameObject star3red;
    [SerializeField]
    private GameObject star3blue;
    [SerializeField]
    private bool light3on;

    [SerializeField]
    private GameObject star4red;
    [SerializeField]
    private GameObject star4blue;
    [SerializeField]
    private bool light4on;

    [SerializeField]
    private GameObject star5red;
    [SerializeField]
    private GameObject star5blue;
    [SerializeField]
    private bool light5on;

    [SerializeField]
    private GameObject greyStars;

    [SerializeField]
    private int m_TimeSecModulo;

    [SerializeField]
    private float m_TimeBetween;

    private float m_TimeLight;

    private float m_TimePolice;

    // Start is called before the first frame update
    void Start()
    {
        Reset();

        src.clip = policeSong;
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeLight += Time.deltaTime;

        if (m_HasPolice)
        {
            if (!src.isPlaying)
            {
                src.Play();
            }
        }
        else
        {
            src.Pause();
        }

        if (m_HasPolice)
        {
            m_TimePolice += Time.deltaTime;
        }
        if (m_TimePolice >= m_TimeWaitPoliceGone)
        {
            m_TimePolice = 0;
            AbstractStars(1);
        }

        if (m_HasPolice)
        {
            if (stars == 1)
            {
                light1on = true;
                light2on = false;
                light3on = false;
                light4on = false;
                light5on = false;
                greyStars.gameObject.SetActive(true);

                star1red.gameObject.SetActive(true);
                star2red.gameObject.SetActive(false);
                star3red.gameObject.SetActive(false);
                star4red.gameObject.SetActive(false);
                star5red.gameObject.SetActive(false);
                star1blue.gameObject.SetActive(true);
                star2blue.gameObject.SetActive(false);
                star3blue.gameObject.SetActive(false);
                star4blue.gameObject.SetActive(false);
                star5blue.gameObject.SetActive(false);
            }
            if (stars == 2)
            {
                light1on = true;
                light2on = true;
                light3on = false;
                light4on = false;
                light5on = false;
                greyStars.gameObject.SetActive(true);

                star1red.gameObject.SetActive(true);
                star2red.gameObject.SetActive(true);
                star3red.gameObject.SetActive(false);
                star4red.gameObject.SetActive(false);
                star5red.gameObject.SetActive(false);
                star1blue.gameObject.SetActive(true);
                star2blue.gameObject.SetActive(true);
                star3blue.gameObject.SetActive(false);
                star4blue.gameObject.SetActive(false);
                star5blue.gameObject.SetActive(false);
            }
            if (stars == 3)
            {
                light1on = true;
                light2on = true;
                light3on = true;
                light4on = false;
                light5on = false;
                greyStars.gameObject.SetActive(true);

                star1red.gameObject.SetActive(true);
                star2red.gameObject.SetActive(true);
                star3red.gameObject.SetActive(true);
                star4red.gameObject.SetActive(false);
                star5red.gameObject.SetActive(false);
                star1blue.gameObject.SetActive(true);
                star2blue.gameObject.SetActive(true);
                star3blue.gameObject.SetActive(true);
                star4blue.gameObject.SetActive(false);
                star5blue.gameObject.SetActive(false);
            }
            if (stars == 4)
            {
                light1on = true;
                light2on = true;
                light3on = true;
                light4on = true;
                light5on = false;
                greyStars.gameObject.SetActive(true);

                star1red.gameObject.SetActive(true);
                star2red.gameObject.SetActive(true);
                star3red.gameObject.SetActive(true);
                star4red.gameObject.SetActive(true);
                star5red.gameObject.SetActive(false);
                star1blue.gameObject.SetActive(true);
                star2blue.gameObject.SetActive(true);
                star3blue.gameObject.SetActive(true);
                star4blue.gameObject.SetActive(true);
                star5blue.gameObject.SetActive(false);
            }
            if (stars == 5)
            {
                light1on = true;
                light2on = true;
                light3on = true;
                light4on = true;
                light5on = true;
                greyStars.gameObject.SetActive(true);

                star1red.gameObject.SetActive(true);
                star2red.gameObject.SetActive(true);
                star3red.gameObject.SetActive(true);
                star4red.gameObject.SetActive(true);
                star5red.gameObject.SetActive(true);
                star1blue.gameObject.SetActive(true);
                star2blue.gameObject.SetActive(true);
                star3blue.gameObject.SetActive(true);
                star4blue.gameObject.SetActive(true);
                star5blue.gameObject.SetActive(true);
            }

            if (light1on)
            {
                if (m_TimeLight % m_TimeSecModulo <= m_TimeBetween)
                {
                    star1red.SetActive(true);
                    star1blue.SetActive(false);
                }
                if (m_TimeLight % m_TimeSecModulo > m_TimeBetween)
                {
                    star1blue.SetActive(true);
                    star1red.SetActive(false);
                }
            }
            if (light2on)
            {
                if (m_TimeLight % m_TimeSecModulo <= m_TimeBetween)
                {
                    star2red.SetActive(false);
                    star2blue.SetActive(true);
                }
                if (m_TimeLight % m_TimeSecModulo > m_TimeBetween)
                {
                    star2blue.SetActive(false);
                    star2red.SetActive(true);
                }
            }
            if (light3on)
            {
                if (m_TimeLight % m_TimeSecModulo <= m_TimeBetween)
                {
                    star3red.SetActive(true);
                    star3blue.SetActive(false);
                }
                if (m_TimeLight % m_TimeSecModulo > m_TimeBetween)
                {
                    star3blue.SetActive(true);
                    star3red.SetActive(false);
                }
            }
            if (light4on)
            {
                if (m_TimeLight % m_TimeSecModulo <= m_TimeBetween)
                {
                    star4red.SetActive(false);
                    star4blue.SetActive(true);
                }
                if (m_TimeLight % m_TimeSecModulo > m_TimeBetween)
                {
                    star4blue.SetActive(false);
                    star4red.SetActive(true);
                }
            }
            if (light5on)
            {
                if (m_TimeLight % m_TimeSecModulo <= m_TimeBetween)
                {
                    star5red.SetActive(true);
                    star5blue.SetActive(false);
                }
                if (m_TimeLight % m_TimeSecModulo > m_TimeBetween)
                {
                    star5blue.SetActive(true);
                    star5red.SetActive(false);
                }
            }
        }

        if (stars > 0)
        {
            m_HasPolice = true;
        }
        else
        {
            if (m_HasPolice)
            {
                m_HasPolice = false;
                greyStars.gameObject.SetActive(false);
                star1red.gameObject.SetActive(false);
                star2red.gameObject.SetActive(false);
                star3red.gameObject.SetActive(false);
                star4red.gameObject.SetActive(false);
                star5red.gameObject.SetActive(false);
                star1blue.gameObject.SetActive(false);
                star2blue.gameObject.SetActive(false);
                star3blue.gameObject.SetActive(false);
                star4blue.gameObject.SetActive(false);
                star5blue.gameObject.SetActive(false);
            }
        }
    }

    public void AddStars(int amount)
    {
        m_HasPolice = true;

        if (stars+ amount <= m_MaxStars)
        {
            stars += amount;
        }
        else
        {
            stars = m_MaxStars;
        }
    }

    public void AbstractStars(int amount)
    {
        if (stars - amount > 0)
        {
            stars -= amount;
        }
        else
        {
            Reset();
        }
    }

    public int GetStars()
    {
        return stars;
    }

    public bool HasStars()
    {
        return m_HasPolice;
    }

    public void Reset()
    {
        m_HasPolice = false;
        stars = 0;
        greyStars.gameObject.SetActive(false);
        star1red.gameObject.SetActive(false);
        star2red.gameObject.SetActive(false);
        star3red.gameObject.SetActive(false);
        star4red.gameObject.SetActive(false);
        star5red.gameObject.SetActive(false);
        star1blue.gameObject.SetActive(false);
        star2blue.gameObject.SetActive(false);
        star3blue.gameObject.SetActive(false);
        star4blue.gameObject.SetActive(false);
        star5blue.gameObject.SetActive(false);
    }
}
