using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [Header("Stats")]
    public static HP Instance;
    public float Hp;
    [SerializeField]
    private float Max_HP;

    [SerializeField]
    private RectTransform HpBar;

    [SerializeField]
    private float HungerOrThirstDecaySize;

    [Header("Ui")]
    [SerializeField]
    private GameObject DeadUi;
    [SerializeField]
    private GameObject BloodUi;
    [SerializeField]
    private float m_TimeShowBlood;
    private float m_TimeBlood;
    private bool m_ShowBlood;

    [Header("Audio")]
    [SerializeField]
    private AudioSource playerAudioSrc;
    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioSource playerDeadSrc;
    [SerializeField]
    private AudioClip deadSound;

    private float m_PercentUnit;

    private bool m_ThirstDecay;
    private bool m_HungerDecay;

    private bool m_IsAlive = true;

    private float m_TotalTime;
    private float m_TotalShowedTimeUi = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Hp = Max_HP;
        m_PercentUnit = 1f / HpBar.anchorMax.x;
        DeadUi.SetActive(false);

        playerAudioSrc.clip = hitSound;

        playerDeadSrc.clip = deadSound;

        BloodUi.SetActive(false);
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp < 0.0f) Hp = 0.0f;
        else if (Hp > Max_HP) Hp = Max_HP;
        HpBar.anchorMax = new Vector2(Hp * m_PercentUnit / 100f, HpBar.anchorMax.y);

        if (m_ShowBlood)
        {
            m_TimeBlood+= Time.deltaTime;

            if(m_TimeBlood>= m_TimeShowBlood)
            {
                m_ShowBlood = false;
                BloodUi.SetActive(false);
                m_TimeBlood = 0;
            }
        }

        if (m_HungerDecay|| m_ThirstDecay)
        {
            Hp -= HungerOrThirstDecaySize * Time.deltaTime;
        }

        if (Hp <= 0.0f)
        {
            Die();
            m_IsAlive = false;
        }

        if (m_IsAlive == false)
        {
            m_TotalTime += Time.deltaTime;

            if (m_TotalTime > m_TotalShowedTimeUi)
            {
                DeadUi.gameObject.SetActive(false);
                m_TotalTime = 0;
                m_IsAlive = true;
            }
        }
    }

    void Die()
    {
        m_ShowBlood = false;
        BloodUi.SetActive(false);
        m_TimeBlood = 0;

        DeadUi.SetActive(true);

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerDeadSrc.Play();
        RestoreHp();
    }

    public void StartHungerDecay()
    {
       m_HungerDecay = true;
    }
    public void StopHungerDecay()
    {
        m_HungerDecay = false;
    }

    public void StartThirstDecay()
    {
        m_ThirstDecay = true;
    }
    public void StopThirstDecay()
    {
        m_ThirstDecay = false;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        playerAudioSrc.pitch=Random.Range(0.8f, 1.2f);
        playerAudioSrc.Play();

        BloodUi.SetActive(true);
        m_ShowBlood = true;
        m_TimeBlood = 0;
    }

    public void RestoreHp()
    {
        Hp = Max_HP;
    }
}
