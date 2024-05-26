using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField]
    private GameObject DiedUi;
    [SerializeField]
    private GameObject FadeUi;
    [SerializeField]
    private CanvasGroup canvas;

    private GameObject m_Player;
    private GameObject[] m_AllNPCS;
    private GameObject[] m_AllCars;

    [SerializeField]
    private float m_CashLossMinMultipl;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_AllNPCS = GameObject.FindGameObjectsWithTag("NPC");
        m_AllCars = GameObject.FindGameObjectsWithTag("Cars");
    }

    public void Sleep()
    {
        Reset();
        Stars.Instance.Reset();
    }
    public void Restart()
    {
        Sleep();

        //resseting player
        int cash = (int)(Cash.Instance.GetCash() * m_CashLossMinMultipl);
        Cash.Instance.AbstractCash(cash);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        DiedUi.SetActive(false);

        ThirdPersonController controller= m_Player.GetComponent<ThirdPersonController>();
        controller?.Reset();

        Time.timeScale = 1;
    }

    public void Reset()
    {
        //resetting npcs
        for (int index = 0; index < m_AllNPCS.Length; index++)
        {
            NPCSHp checkNPC = m_AllNPCS[index].GetComponent<NPCSHp>();

            checkNPC?.Reset();
        }

        //resetting cars
        for (int index = 0; index < m_AllCars.Length; index++)
        {
            Cars checkCar = m_AllCars[index].GetComponent<Cars>();

            checkCar?.Reset();

        }

        ThirdPersonController controller = m_Player.GetComponent<ThirdPersonController>();
        controller?.Reset();
    }
}
