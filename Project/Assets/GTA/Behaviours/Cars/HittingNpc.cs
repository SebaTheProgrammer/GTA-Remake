using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingNpc : MonoBehaviour
{
    private Transform m_Car;
    private bool m_Once=true;
    private void Start()
    {
        m_Car = this.transform;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "NPC")
        {
            if (col.transform.TryGetComponent(out NPCSHp enemy) && enemy.IsAlive()&& m_Once)
            {
                enemy.TakeDamage(100);
               // GameObject hitted = Instantiate(enemy.gameObject, m_Car.position, m_Car.rotation);

               // Rigidbody rb = hitted.GetComponent<Rigidbody>();
                //rb.AddForce(m_Car.forward*1000);
               // m_Once = false;

                //enemy.gameObject.GetComponent(Rigidbody<);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        m_Once = true;
    }
}
