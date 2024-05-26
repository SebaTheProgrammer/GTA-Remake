using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisepearBloks : MonoBehaviour
{
    private Renderer m_Render;
    void Start()
    {
        m_Render = this.gameObject.GetComponent<Renderer>();
        m_Render.enabled = false;
    }
}
