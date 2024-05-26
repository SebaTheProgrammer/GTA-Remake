using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcBehaviour : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private NPCSHp NPCsHp;

    [SerializeField]
    private float amountOfDamageDealing;

    [SerializeField]
    private bool isPolice;

    [Header("Patrol")]
    [SerializeField]
    private bool m_StandStill;

    [SerializeField]
    private GameObject[] patrolPoints;

    [SerializeField]
    private int targetPoint;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float angrySpeed=7;

    [SerializeField]
    private float roundingAroundPatrolPoints;

    private NavMeshAgent m_NavMeshAgent;

    private GameObject m_Player;

    [Header("Animator")]
    [SerializeField]
    private Animator animator;
    private int animIDRun;
    private int animIDWalk;
    private int animIDAttack;

    private bool m_CanHitPlayer;

    [Header("Attack")]
    [SerializeField]
    private float m_AttackSpeed;
    private float m_LastTimeAttack;

    private int m_AttackOrRun;

    private bool m_StopWalking;

    private bool m_StopWalkingCar;

    [Header("Wait/Idle")]
    [SerializeField]
    private float m_WaitTimeIdle = 3;
    private float m_TotalTimeIdle;

    [SerializeField]
    private float m_WaitTimeIdleCars = 20;
    private float m_TotalTimeIdleCars;

    [Header("Police")]
    [SerializeField]
    private Stars m_Stars;
    [SerializeField]
    private float m_SearchRadius = 30;

    private bool m_IsInSearchDistance;
    private Rigidbody m_RigidBody;

    private Vector3 m_StartPos;


  void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_RigidBody = GetComponent<Rigidbody>();
        if (!m_StandStill)
        {
            patrolPoints = GameObject.FindGameObjectsWithTag("NPCPatrolPoints");
            targetPoint = Random.Range(0, patrolPoints.Length);
        }
        m_Stars = GetComponentInParent<Stars>();

        m_NavMeshAgent.speed = speed;

        AssignAnimationIDs();

        if (!m_StandStill)
        {
            m_AttackOrRun = Random.Range(0, 2);
        }
        else
        {
            m_AttackOrRun = 0;
        }

        m_StartPos = transform.position;
    }

    void Update()
    {
        if (NPCsHp.IsAlive())
        {
            CalculateIsInSearchDistance();

            if (m_Stars.HasStars() && m_IsInSearchDistance)
            {
                if (isPolice)
                {
                    Fight();
                }
                else
                {
                    if (!m_StandStill)
                    {
                        RunAway();
                    }
                }
            }
            else if (NPCsHp.FollowPlayer())
            {
                if (m_AttackOrRun == 0 || isPolice)
                {
                    Fight();
                }
                else
                {
                    RunAway();
                }
            }
            else
            {
                if (!m_StandStill)
                {
                    Walk();
                }
                else
                {
                    if (m_StandStill)
                    {
                        if (Vector3.Distance(this.gameObject.transform.position,m_StartPos)>=0.5f)
                        {
                            Vector3 distance = m_StartPos - this.gameObject.transform.position;
                            Vector3 toTarget = (distance).normalized;
                            this.gameObject.transform.position += toTarget * Time.deltaTime * angrySpeed;
                            this.gameObject.transform.LookAt(m_StartPos);
                        }
                        else
                        {
                            animator.SetTrigger("idle");
                            animator.SetBool(animIDRun, false);
                            animator.SetBool(animIDWalk, true);
                            m_NavMeshAgent.speed = 0;
                        }
                    }
                    else
                    {
                        Idle();
                    }
                }
            }
        }
        else
        {
            m_NavMeshAgent.speed = 0;
        }
    }

    private void Idle()
    {
        animator.SetTrigger("idle");
        animator.SetBool(animIDRun, false);
        animator.SetBool(animIDWalk, true);
        m_NavMeshAgent.speed = 0;

        if (!m_StopWalkingCar)
        {
            m_TotalTimeIdle += Time.deltaTime;

            if (m_TotalTimeIdle >= m_WaitTimeIdle)
            {
                m_StopWalking = false;
                m_TotalTimeIdle = 0;
            }
        }
        else
        {
            m_TotalTimeIdleCars += Time.deltaTime;

            if (m_TotalTimeIdleCars >= m_WaitTimeIdleCars)
            {
                m_StopWalkingCar = false;
                m_TotalTimeIdleCars = 0;
            }
        }
    }

    private void Walk()
    {
        if (m_StopWalkingCar)
        {
            Idle();
        }
        else if (!m_StopWalking)
        {
            if (!m_StandStill)
            {
                m_NavMeshAgent.enabled = true;

                float distance = Vector3.Distance(this.transform.position, patrolPoints[targetPoint].transform.position);

                if (distance <= roundingAroundPatrolPoints)
                {
                    targetPoint = Random.Range(0, patrolPoints.Length);
                }
                animator.SetTrigger("walking");
                animator.SetBool(animIDRun, false);
                animator.SetBool(animIDWalk, true);

                m_NavMeshAgent.destination = patrolPoints[targetPoint].transform.position;
                m_NavMeshAgent.speed = speed;
            }
        }
        else
        {
            Idle();
        }
    }
    private void Fight()
    {
        m_NavMeshAgent.enabled = false;

        animator.SetTrigger("running");
        animator.SetBool(animIDWalk, false);
        animator.SetBool(animIDRun, true);

        if (!m_CanHitPlayer && !animator.GetBool(animIDAttack))
        {
            Vector3 distance = m_Player.transform.position - this.gameObject.transform.position;
            Vector3 toTarget = (distance).normalized;
            this.gameObject.transform.position += toTarget * Time.deltaTime * angrySpeed;

            //m_RigidBody.position += toTarget * Time.deltaTime * angrySpeed;
            this.gameObject.transform.LookAt(m_Player.transform);
        }

        if (m_CanHitPlayer)
        {
            animator.SetTrigger("attack");
            animator.SetBool(animIDAttack, true);

            if (m_LastTimeAttack >= m_AttackSpeed)
            {
                m_LastTimeAttack = 0;
                HP.Instance.TakeDamage(amountOfDamageDealing);
            }

            m_LastTimeAttack += Time.deltaTime;
        }
        else
        {
            animator.SetBool(animIDAttack, false);
        }
    }
    private void RunAway()
    {
        m_NavMeshAgent.enabled = true;

        animator.SetTrigger("running");
        animator.SetBool(animIDWalk, false);
        animator.SetBool(animIDRun, true);

        m_NavMeshAgent.speed = angrySpeed;

        m_NavMeshAgent.destination += m_Player.transform.position;
    }
    private void CalculateIsInSearchDistance()
    {
        float distance = (this.gameObject.transform.position - m_Player.transform.position).magnitude;

        if (m_SearchRadius >= distance)
        {
            m_IsInSearchDistance = true;
        }
        else
        {
            m_IsInSearchDistance = false;
        }
    }
    private void AssignAnimationIDs()
    {
        animIDRun = Animator.StringToHash("run");
        animIDWalk = Animator.StringToHash("walk");
        animIDAttack = Animator.StringToHash("attack");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_CanHitPlayer = true;
            m_StopWalking = true;
        }

        if (col.gameObject.tag == "Cars")
        {
            m_StopWalkingCar = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_CanHitPlayer = false;
            m_StopWalking = false;
            m_TotalTimeIdle = 0;
        }

        if (col.gameObject.tag == "Cars")
        {
            m_StopWalkingCar = false;
            m_TotalTimeIdleCars = 0;
        }
    }

    public void ChangeAttackOrRun(int value)
    {
        m_AttackOrRun = value;
    }
}
