using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsEnterAndExit : MonoBehaviour
{
    [SerializeField]
    private string AcceptButton;

    [Header("Scipts")]
    private CarController m_CarControllMovenment;
    private MonoBehaviour m_PathOfCar;
    private RightSide m_Right;
    private HittingNpc m_HittingNPC;
    private NPCSStop m_NpcStop;
    private PhoneScript m_PhoneScript;

    [Header("Objects")]
    private GameObject m_Car;
    private GameObject m_Player;
    private CinemachineVirtualCamera m_CarCam;
    private CarRadio m_Radio;

    [Header("Ui")]
    private GameObject m_PlayerUi;
    private GameObject m_CarUi;

    private bool m_Candrive;
    private bool m_IsInCar;

    void Awake()
    {
        m_Car=this.gameObject;

        m_Right = GetComponentInChildren<RightSide>();
        m_NpcStop = GetComponentInChildren<NPCSStop>();
        m_HittingNPC = GetComponentInChildren<HittingNpc>();

        m_HittingNPC?.gameObject.SetActive(false);
        m_CarCam = m_Car.GetComponentInChildren<CinemachineVirtualCamera>();

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerUi = GameObject.FindGameObjectWithTag("PlayerUi");
        m_CarControllMovenment = GetComponent<CarController>();
        m_CarControllMovenment.enabled = false;

        m_PathOfCar = GetComponent<Cars>();;

        m_CarUi = GameObject.FindGameObjectWithTag("CarUi");
        m_Radio = GetComponentInParent<CarRadio>();

        m_PhoneScript= GetComponentInParent<PhoneScript>();
    }

    void Update()
    {
        //enter
        float distance = Vector3.Distance(m_Player.transform.position, this.gameObject.transform.position);

        //exit
        if (Input.GetKeyDown(AcceptButton) && m_IsInCar)
        {
            m_IsInCar = false;
            m_CarCam.Priority = 1;

            m_CarControllMovenment.OutOfCar();
            m_CarControllMovenment.enabled = false;

            m_Player.transform.SetParent(null);
            m_Player.gameObject.SetActive(true);

            m_PlayerUi.gameObject.SetActive(true);
            m_HittingNPC.gameObject.SetActive(false);
            m_CarUi.gameObject.SetActive(false);

            m_Radio.StopRadio();
            m_Radio.enabled = false;
            m_PhoneScript.enabled = true;
        }
        else if (Input.GetKeyDown(AcceptButton) && m_Candrive&& distance <= 5)
        {
            m_CarCam.Priority = 1000;

            m_CarControllMovenment.enabled = true;

            m_PathOfCar.enabled = false;

            m_PlayerUi?.gameObject.SetActive(false);

            m_Player.transform.SetParent(m_Car.transform);
            m_Player.gameObject.SetActive(false);

            m_Right.gameObject.SetActive(false);
            m_NpcStop.gameObject.SetActive(false);
            m_HittingNPC.gameObject.SetActive(true);
            m_CarUi.gameObject.SetActive(true);
            m_IsInCar = true;
            m_Radio.enabled = true;
            m_PhoneScript.StopRadio();
            m_PhoneScript.enabled = false;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_Candrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_Candrive = false;
        }
    }
}
