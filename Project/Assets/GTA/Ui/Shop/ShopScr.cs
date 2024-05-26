using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScr : MonoBehaviour
{
    [SerializeField]
    private string EnterButton;
    [SerializeField]
    private string Pause;

    [Header("Ui")]
    [SerializeField]
    private GameObject ShopUi;
    [SerializeField]
    private GameObject PlayerUi;
    [SerializeField]
    private GameObject MoneyUi;
    [SerializeField]
    private GameObject EnterUi;
    [SerializeField]
    private GameObject TimeUi;

    [SerializeField]
    private GameObject ShopBar1;
    [SerializeField]
    private GameObject ShopBar2;
    [SerializeField]
    private GameObject ShopBar3;
    [SerializeField]
    private GameObject ShopBar4;

    [SerializeField]
    private MonoBehaviour player;

    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip bg;

    private bool m_ShopOnline;
    private bool m_CanEnter;

    void Start()
    {
        ShopUi.gameObject.SetActive(false);
        TimeUi.gameObject.SetActive(false);
        m_ShopOnline = false;

        player.enabled = true;

        src.clip = bg;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(EnterButton) && m_CanEnter)
        {
            if (m_ShopOnline == false)
            {
                EnterUi.gameObject.SetActive(false);
                ShopUi.gameObject.SetActive(true);
                PlayerUi.gameObject.SetActive(false);
                player.enabled = false;

                m_ShopOnline = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                ShopBar1.gameObject.SetActive(true);
                ShopBar2.gameObject.SetActive(false);
                ShopBar3.gameObject.SetActive(false);
                ShopBar4.gameObject.SetActive(false);

                EnterUi.gameObject.SetActive(false);
                MoneyUi.gameObject.SetActive(true);
                TimeUi.gameObject.SetActive(true);

                src.pitch = Random.Range(0.8f, 1.0f);
                src.Play();
            }
            else
            {
                ExitShop();
            }
        }
        if (Input.GetKeyDown(Pause))
        {
            if (m_ShopOnline)
            {
                ExitShop();
            }
        }
    }

    public void ExitShop()
    {
        ShopUi.gameObject.SetActive(false);
        m_ShopOnline = false;
        player.enabled = true;
        PlayerUi.gameObject.SetActive(true);
        MoneyUi.gameObject.SetActive(false);
        EnterUi.gameObject.SetActive(true);
        TimeUi.gameObject.SetActive(false);
        m_CanEnter = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        src.Stop();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!m_CanEnter)
            {
                EnterUi.gameObject.SetActive(true);
                m_CanEnter = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EnterUi.gameObject.SetActive(false);
            m_CanEnter = false;
        }
    }
}
