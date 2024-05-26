using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float timeMultiplier;
    [SerializeField]
    private float startHour;
    [SerializeField]
    private float sunsetHour;
    [SerializeField]
    private float sunriseHour;
    [SerializeField]
    private float HourGettingDailyCash;

    [Header("Objects")]
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI m_TimeTextPhone;
    [SerializeField]
    private Light sunLight;
    [SerializeField]
    private GameObject moonLight;
    [SerializeField]
    private GameObject stars;
    [SerializeField]
    private GameObject m_UiDailyCash;
    [SerializeField]
    private TMP_Text m_DailyCashText;

    private TimeSpan m_MoneyTime;
    private TimeSpan m_MoneyTime1;
    private DateTime m_CurrentTime;
    private TimeSpan m_SunriseTime;
    private TimeSpan m_SunsetTime;
    private bool m_AddCashOnce;

    private float m_ExtraSleepTime;
    [SerializeField]
    private float m_TimeDailyUiShow;
    private float m_TimeDailyUi;

    // Start is called before the first frame update
    void Start()
    {
        m_ExtraSleepTime = 0;
        m_CurrentTime = DateTime.Now.Date+TimeSpan.FromHours(startHour);
        m_SunriseTime = TimeSpan.FromHours(sunriseHour);
        m_SunsetTime = TimeSpan.FromHours(sunsetHour);

        m_MoneyTime = TimeSpan.FromHours(HourGettingDailyCash);
        m_MoneyTime1 = TimeSpan.FromHours(HourGettingDailyCash+1);
        m_AddCashOnce = true;
        m_UiDailyCash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();

        if (m_UiDailyCash.activeSelf)
        {
            m_TimeDailyUi += Time.deltaTime;
        }
        if (m_TimeDailyUi >= m_TimeDailyUiShow)
        {
            m_TimeDailyUi = 0;
            m_UiDailyCash.SetActive(false);
        }
    }

    private void UpdateTimeOfDay()
    {
        m_CurrentTime = m_CurrentTime.AddSeconds(Time.deltaTime * timeMultiplier + m_ExtraSleepTime);

        if (timeText != null)
        {
            timeText.text = m_CurrentTime.ToString("HH:mm");
            m_TimeTextPhone.text = m_CurrentTime.ToString("HH:mm");
        }

        if (m_CurrentTime.TimeOfDay >= m_MoneyTime && m_CurrentTime.TimeOfDay <= m_MoneyTime1
            && m_AddCashOnce)
        {
            DoDailyCashAndRepu();
        }

        m_ExtraSleepTime = 0;
    }
    public void AddTime(float amount)
    {
        m_ExtraSleepTime = amount;
    }
    public bool CanSleep()
    {
        if (m_CurrentTime.TimeOfDay < m_SunriseTime || m_CurrentTime.TimeOfDay > m_SunsetTime)
        {
            return true;
        }
        else return false;
    }
    private void DoDailyCashAndRepu()
    {
        Cash.Instance.AddCash(Cash.Instance.GetDailyCash());
        Cash.Instance.AddReputationBuildings();
        m_DailyCashText.text = "$" + Cash.Instance.GetDailyCash().ToString();
        m_UiDailyCash.SetActive(true);
        m_AddCashOnce = false;
    }

    private void RotateSun()
    {
        float sunLightRotation;
        float moonLightRotation;

        if (m_CurrentTime.TimeOfDay > m_SunriseTime && m_CurrentTime.TimeOfDay < m_SunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(m_SunriseTime, m_SunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(m_SunriseTime, m_CurrentTime.TimeOfDay);

            double percantage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percantage);
            moonLightRotation = Mathf.Lerp(0, 180, (float)percantage);
            sunLight.intensity = 1;

            stars.gameObject.SetActive(false);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(m_SunsetTime, m_SunriseTime);
            TimeSpan timeSinceSunSet = CalculateTimeDifference(m_SunsetTime, m_CurrentTime.TimeOfDay);

            double percantage = timeSinceSunSet.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percantage);
            moonLightRotation = Mathf.Lerp(180, 360, (float)percantage);
            sunLight.intensity = 0;

            stars.gameObject.SetActive(true);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);

        moonLight.transform.rotation = Quaternion.AngleAxis(moonLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}
