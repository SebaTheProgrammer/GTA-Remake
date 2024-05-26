using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_PatrolPoints;
    [SerializeField]
    private int m_TargetPoint;
    [SerializeField]
    private float m_Speed;

    private int m_TargetStartPoint;
    private Vector3 m_StartPos;
    private Quaternion m_StartRot;

    private RightSide m_Right;
    private NPCSStop m_NpcStop;

    // Start is called before the first frame update
    void Start()
    {
        m_TargetStartPoint = m_TargetPoint;
        m_StartPos=this.transform.position;
        m_StartRot=this.gameObject.transform.rotation;
        m_Right = GetComponentInChildren<RightSide>();
        m_NpcStop = GetComponentInChildren<NPCSStop>();
        m_Right?.gameObject.SetActive(false);
        m_NpcStop?.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PatrolPoints.Length > 0)
        {
            m_Right?.gameObject.SetActive(true);
            m_NpcStop?.gameObject.SetActive(true);

            if (Vector3.Distance(transform.position, m_PatrolPoints[m_TargetPoint].position) <= 1)
            {
                increaseTargetInt();
            }

            transform.position = Vector3.MoveTowards(transform.position, m_PatrolPoints[m_TargetPoint].position, m_Speed * Time.deltaTime);
            transform.LookAt(m_PatrolPoints[m_TargetPoint].position);
        }
    }

    void increaseTargetInt()
    {
        m_TargetPoint += 1;

        if (m_TargetPoint >= m_PatrolPoints.Length)
        {
            m_TargetPoint = 0;
        }

    }

    public void Reset()
    {
        m_TargetPoint = m_TargetStartPoint;
        this.transform.position = m_StartPos;
        this.transform.transform.rotation = m_StartRot;
    }

    public int GetSizeOfPatrolPoints()
    {
        return m_PatrolPoints.Length;
    }

}
