using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRadio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip[] songs;

    [Header("Buttons")]
    [SerializeField]
    private string PlayAndStopButton;
    [SerializeField]
    private string NextButton;
    [SerializeField]
    private string PrevButton;

    private bool m_PlayMusic;

    private int m_IndexWichSong = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayMusic)
        {
            if (!src.isPlaying)
            {
                src.Play();
            }
        }

        if (Input.GetKeyDown(PlayAndStopButton) && m_PlayMusic)
        {
            StopRadio();
        }
        else if(Input.GetKeyDown(PlayAndStopButton) && !m_PlayMusic)
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

    private void PlayRadio()
    {
        src.clip = songs[m_IndexWichSong];
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
            src.clip = songs[m_IndexWichSong];
        }
        else
        {
            m_IndexWichSong = 0;
        }
    }

    private void PrevSong()
    {
        if ((m_IndexWichSong) > 0)
        {
            m_IndexWichSong -= 1;
            src.clip = songs[m_IndexWichSong];
        }
        else
        {
            m_IndexWichSong = songs.Length;
        }
    }
}
