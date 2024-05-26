using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class NPCSHp : MonoBehaviour
{
    private float health;

    [Header("Stats")]
    [SerializeField]
    private float starthealth;
    [SerializeField]
    private int m_MoneyDropMax = 20;
    [SerializeField]
    private bool isFemale;

    [Header("Objects")]
    [SerializeField]
    private Animator Animator;
    private GameObject mainCamera;

    [Header("Markers")]
    [SerializeField]
    private float TimeShowHitmarker;
    private float timeHitMarker;
    private bool showHit;
    [SerializeField]
    private GameObject hitMarker;

    [SerializeField]
    private float totalTimeBeAngry;
    private float timeBeAngry;
    private bool showAngry;
    private bool m_QuestAngry;
    [SerializeField]
    private GameObject angryMarker;

    [SerializeField]
    private BoxCollider boxCollider;

    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSrc;
    [SerializeField]
    private AudioClip hitSoundMale;
    [SerializeField]
    private AudioClip hitSoundFemale;

    private Vector3 m_StartLocation;
    private bool m_IsALive = true;
    private bool m_FollowPlayer;

    private bool m_CanRespawn = true;

    void Start()
    {
        health = starthealth;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        m_StartLocation=this.transform.position;

        audioSrc = GetComponentInParent<AudioSource>();

        if (hitMarker != null)
        {
            hitMarker?.SetActive(false);
        }
        if (angryMarker != null)
        {
            angryMarker?.SetActive(false);
        }
    }
    private void Update()
    {
        //hit
        if (showHit)
        {
            timeHitMarker += Time.deltaTime;
            if (hitMarker != null)
            {
                hitMarker?.SetActive(true);
            }
            hitMarker.transform.position = this.gameObject.transform.position;
            hitMarker.transform.rotation = mainCamera.transform.rotation;
            m_FollowPlayer = true;
        }
        if (timeHitMarker > TimeShowHitmarker)
        {
            showHit = false;
            if (hitMarker != null)
            {
                hitMarker?.SetActive(false);
            }
            timeHitMarker = 0;
            showAngry = true;
        }

        //angry
        if (showAngry || m_QuestAngry)
        {
            timeBeAngry += Time.deltaTime;
            if (angryMarker != null)
            {
                angryMarker?.SetActive(true);
            }
            angryMarker.transform.position = this.gameObject.transform.position;
            angryMarker.transform.rotation = mainCamera.transform.rotation;

            if (hitMarker != null)
            {
                hitMarker?.SetActive(false);
            }

            m_FollowPlayer = true;
        }
        if (timeBeAngry > totalTimeBeAngry)
        {
            showAngry = false;
            if (angryMarker != null)
            {
                angryMarker?.SetActive(false);
            }
            timeBeAngry = 0;
            m_FollowPlayer = false;
        }

    }
    public void TakeDamage(float damageAmount)
    {
        if (m_IsALive)
        {
            if (isFemale)
            {
                if (!audioSrc.isPlaying)
                {
                    audioSrc.clip = hitSoundFemale;
                    audioSrc.pitch = Random.Range(0.9f, 1.1f);
                    audioSrc.Play();
                }
            }
            else
            {
                if (!audioSrc.isPlaying)
                {
                    audioSrc.clip = hitSoundMale;
                    audioSrc.pitch = Random.Range(0.9f, 1.1f);
                    audioSrc.Play();
                }
            }
            health -= damageAmount;

            showHit = true;
            showAngry = false;
            angryMarker.SetActive(false);
            m_FollowPlayer = true;

            if (health <= 0 && m_IsALive)
            {
                Die();
            }
        }
    }
    public bool IsAlive()
    {
        return m_IsALive;
    }

    public bool FollowPlayer()
    {
        return m_FollowPlayer;
    }

    void Die()
    {
        m_IsALive = false;
        int cash = Random.Range(1, m_MoneyDropMax);
        Cash.Instance.AddCash(cash);

        Reputation.Instance.Abstract();

        Animator.enabled = false;

        Stars.Instance.AddStars(1);

        showHit = false;
        hitMarker?.SetActive(false);
        showAngry = false;
        angryMarker?.SetActive(false);
        timeBeAngry = 0;
        timeHitMarker = 0;
    }

    public void Reset()
    {
        if (m_CanRespawn)
        {
            m_IsALive = true;
            health = starthealth;
            this.gameObject.transform.position = m_StartLocation;
            Animator.enabled = true;
        }

        m_FollowPlayer = false;
        m_QuestAngry = false;
        showHit = false;
        showAngry = false;
        timeBeAngry = 0;
        timeHitMarker = 0;

        if (hitMarker != null)
        {
            hitMarker?.SetActive(false);
        }
        if (angryMarker != null)
        {
            angryMarker?.SetActive(false);
        }
    }

    public void GetAngry()
    {
        showAngry = true;
    }

    public void GetQuestAngry()
    {
        m_QuestAngry = true;
    }

    public void SetNoRespawn()
    {
        m_CanRespawn = false;
    }
}
