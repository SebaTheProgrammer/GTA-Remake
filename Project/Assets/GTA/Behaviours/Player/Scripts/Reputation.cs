using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reputation : MonoBehaviour
{
    public static Reputation Instance;

    [SerializeField]
    private float repu;

    [SerializeField]
    private float startRepu;

    [SerializeField]
    private float MinRepu;

    [SerializeField]
    private float MaxRepu;

    [SerializeField]
    private RectTransform RepBar;

    [SerializeField]
    private float m_MinimumRepuForPopularity;

    private float m_PercentUnit;

    // Start is called before the first frame update
    void Start()
    {
        repu = startRepu;
        m_PercentUnit = 1f / RepBar.anchorMax.x;
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (repu < 0.0f) repu = 0.0f;
        else if (repu > MaxRepu) repu = MaxRepu;

        RepBar.anchorMax = new Vector2(repu * m_PercentUnit / 100f, RepBar.anchorMax.y);
    }

    public void Add()
    {
        ++repu;
    }
    public void Add(int size)
    {
        repu+=size;
    }
    public void Abstract()
    {
        --repu;
    }

    public float GetRepu()
    {
        return repu;
    }

    public float GetMinForPopu()
    {
        return m_MinimumRepuForPopularity;
    }
}
