using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EasterEgg : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_EasterEgg;

    private float m_timerEgg;

    private bool m_Start;

    private void Start()
    {
        //m_EasterEgg.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Start)
        {
            m_timerEgg += Time.deltaTime;
        }

        if (m_timerEgg > 5)
        {
            m_EasterEgg.Play();
            m_Start = false;
            m_timerEgg = 0;
        }
    }

    public void StartSound()
    {
        m_Start = true;
    }
    public void StopSound()
    {
        m_timerEgg = 0;
        m_Start = false;
        if (m_EasterEgg.isPlaying)
        {
            m_EasterEgg.Stop();
        }
    }
}
