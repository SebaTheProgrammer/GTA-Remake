using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    private GameObject m_Cloud;
    private float m_Speed=2;
    private Vector3 m_Origin;

    // Start is called before the first frame update
    void Start()
    {
        m_Cloud = this.gameObject;
        m_Origin = m_Cloud.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_Cloud.transform.position += new Vector3(0, 0, m_Speed * Time.deltaTime);

        if (m_Cloud.transform.position.z > 750)
        {
            m_Cloud.transform.position = new Vector3(m_Origin.x, m_Origin.y, -700);
        }
    }
}
