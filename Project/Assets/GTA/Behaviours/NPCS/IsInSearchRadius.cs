using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInSearchRadius : MonoBehaviour
{
    private bool m_IsInRadius;

    // Start is called before the first frame update
    void Start()
    {

    }
    public bool IsInRadius()
    {
        return m_IsInRadius;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_IsInRadius = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_IsInRadius = false;
        }
    }
}
