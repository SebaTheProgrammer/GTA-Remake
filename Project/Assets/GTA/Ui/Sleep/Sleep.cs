using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField]
    private string m_EnterButton;

    [Header("Ui")]
    [SerializeField]
    private GameObject SleepUi;
    [SerializeField]
    private GameObject FadeUi;
    [SerializeField]
    private CanvasGroup canvas;

    [Header("Time")]
    [SerializeField]
    private TimeManager TimeM;
    [SerializeField]
    private float sleepTime;
    [SerializeField]
    private float TimeToFade;

    [Header("Objects")]
    [SerializeField]
    private GameObject startLocation;
    [SerializeField]
    private GameObject m_Player;

    private bool m_FadeIn;
    private bool m_FadeOut;

    private bool m_CanSleep;

    void Start()
    {
        SleepUi.gameObject.SetActive(false);
        FadeUi.gameObject.SetActive(false);

        m_Player = GameObject.FindGameObjectWithTag("Player");

        startLocation = this.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(m_EnterButton) && m_CanSleep)
        {
            //fade in
            if (TimeM.CanSleep())
            {
                FadeUi.gameObject.SetActive(true);
                m_FadeIn =true;
            }
        }
        //fade in
        if (m_FadeIn)
        {
            FadeIn();
        }
        //fade out
        else if (m_FadeOut)
        {
            FadeOut();
        }

        if (!TimeM.CanSleep()&& m_CanSleep&& m_FadeOut==false)
        {
            SleepUi.gameObject.SetActive(false);
            m_CanSleep = false;
        }
    }

    private void FadeIn()
    {

        if (canvas.alpha < 1)
        {
            canvas.alpha += TimeToFade * Time.deltaTime;

            if (canvas.alpha >= 1)
            {
                m_FadeIn = false;
                TimeM.AddTime(sleepTime);
                m_FadeOut = true;
            }
        }

    }
    private void FadeOut()
    {

        if (canvas.alpha >= 0)
        {
            canvas.alpha -= TimeToFade * Time.deltaTime;

            if (canvas.alpha == 0)
            {
                m_FadeOut = false;
                FadeUi.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (TimeM.CanSleep())
        {
            if (col.gameObject.tag == "Player")
            {
                if (!m_CanSleep)
                {
                    SleepUi.gameObject.SetActive(true);
                    m_CanSleep = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (m_CanSleep)
        {
            if (col.gameObject.tag == "Player")
            {
                SleepUi.gameObject.SetActive(false);
                m_CanSleep = false;
            }
        }
    }
}
