using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScript : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip[] songs;
    [SerializeField]
    private RawImage m_ImageOnPhone;
    [SerializeField]
    private Texture2D[] m_Images;

    [SerializeField]
    private GameObject m_PhoneUi;

    [SerializeField]
    private Quest m_DoneQuestToEarnPhone;


    [Header("Buttons")]
    [SerializeField]
    private string m_PhoneButton;
    [SerializeField]
    private string m_RadioButton;
    [SerializeField]
    private string NextButton;
    [SerializeField]
    private string PrevButton;

    private bool m_IsPhoneOn;
    private bool m_PlayMusic;

    private bool m_CanUsePhone;

    private int m_IndexWichSong = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_PhoneUi?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CanUsePhone)
        {
            if (Input.GetKeyUp(m_PhoneButton) && !m_IsPhoneOn)
            {
                m_IsPhoneOn = true;
                m_PhoneUi?.SetActive(true);
                m_ImageOnPhone.texture = m_Images[m_IndexWichSong];
            }
            else if (Input.GetKeyUp(m_PhoneButton) && m_IsPhoneOn)
            {
                m_IsPhoneOn = false;
                m_PhoneUi?.SetActive(false);
            }

            if (m_IsPhoneOn)
            {
                if (Input.GetKeyDown(m_RadioButton) && m_PlayMusic)
                {
                    StopRadio();
                }
                else if (Input.GetKeyDown(m_RadioButton) && !m_PlayMusic)
                {
                    PlayRadio();
                }

                if (Input.GetKeyDown(NextButton))
                {
                    NextSong();
                }

                if (Input.GetKeyDown(PrevButton))
                {
                    PrevSong();
                }
            }
        }
        else
        {
            if (Cash.Instance.HasBoughtPhone())
            {
                m_CanUsePhone = true;
            }

            if (m_DoneQuestToEarnPhone.HasDoneQuest())
            {
                m_CanUsePhone = true;
            }
        }
    }
    public void CanUsePhone()
    {
        m_CanUsePhone = true;
    }

    private void PlayRadio()
    {
        src.clip = songs[m_IndexWichSong];
        src.Play();
        m_PlayMusic = true;
    }

    public void StopRadio()
    {
        m_PlayMusic = false;
        src.Pause();
        m_IndexWichSong = 0;
    }

    private void NextSong()
    {
        if ((m_IndexWichSong + 1) < songs.Length)
        {
            m_IndexWichSong += 1;
            if (m_PlayMusic)
            {
                src.clip = songs[m_IndexWichSong];
                m_ImageOnPhone.texture = m_Images[m_IndexWichSong];
            }
        }
        else
        {
            m_IndexWichSong = 0;
            if (m_PlayMusic)
            {
                src.clip = songs[m_IndexWichSong];
                m_ImageOnPhone.texture = m_Images[m_IndexWichSong];
            }
        }

        PlayRadio();
    }

    private void PrevSong()
    {
        if ((m_IndexWichSong) > 0)
        {
            m_IndexWichSong -= 1;
            if (m_PlayMusic)
            {
                src.clip = songs[m_IndexWichSong];
                m_ImageOnPhone.texture = m_Images[m_IndexWichSong];
            }
        }
        else
        {
            m_IndexWichSong = songs.Length-1;
            if (m_PlayMusic)
            {
                src.clip = songs[m_IndexWichSong];
                m_ImageOnPhone.texture = m_Images[m_IndexWichSong];
            }
        }

        PlayRadio();
    }
}
