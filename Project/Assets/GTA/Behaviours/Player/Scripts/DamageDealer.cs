using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float weapomLength;
    [SerializeField]
    private float weaponDamage;

    [Header("Audio")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip hitsound;

    private bool canDealDamage;
    List<GameObject> hasDealtDamage;

    // Start is called before the first frame update
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;

            int layerMask = 1 << 9;

            if (Physics.Raycast(transform.position, -transform.up, out hit, weapomLength, layerMask))
            {
                if (hit.transform.TryGetComponent(out NPCSHp enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);

                    src.clip = hitsound;
                    src.pitch = Random.Range(0.8f, 1.3f);
                    src.Play();
                }
            }

        }
    }

    public void StartDealDamage()
    {
        canDealDamage=true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage=false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weapomLength);
    }
}
