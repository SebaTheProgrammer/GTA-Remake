using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5.0f;

    [Header("Keys")]
    [SerializeField]
    private string ForwardKey;
    [SerializeField]
    private string BackwardKey;
    [SerializeField]
    private string LeftKey;
    [SerializeField]
    private string RightKey;

    // Start is called before the first frame update
    void Start()
    {
        m_Speed = 12.0f;
        ForwardKey = "w";
        BackwardKey = "s";
        LeftKey = "a";
        RightKey = "d";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(ForwardKey))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
        }
        if (Input.GetKey(BackwardKey))
        {
            transform.Translate(-1 * Vector3.forward * Time.deltaTime * m_Speed);
        }
        if (Input.GetKey(LeftKey))
        {
            transform.Rotate(0, -Time.deltaTime * m_Speed*10, 0);
        }
        if (Input.GetKey(RightKey))
        {
            transform.Rotate(0, Time.deltaTime * m_Speed*10, 0);
        }
    }
}
