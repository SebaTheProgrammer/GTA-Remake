using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSide : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour m_CarBehavior;

    // Start is called before the first frame update
    void Start()
    {
        m_CarBehavior = GetComponentInParent<Cars>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Cars")
        {
            m_CarBehavior.enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Cars")
        {
            m_CarBehavior.enabled = true;
        }
    }
}
