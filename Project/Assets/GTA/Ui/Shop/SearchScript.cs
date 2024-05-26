using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SearchUi;

    [SerializeField]
    private GameObject LightOpject;

    [SerializeField]
    private int MinimumCash;

    [SerializeField]
    private int MaximumCash;

    [SerializeField]
    private AudioSource src;

    [SerializeField]
    private AudioClip collect;

    private bool m_CanSearch;
    private bool m_SearchOnce = true;

    void Start()
    {
        SearchUi.gameObject.SetActive(false);
        src.clip = collect;
        SearchUi.gameObject.SetActive(false);
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_CanSearch && m_SearchOnce)
        {
            int cash = Random.Range(MinimumCash, MaximumCash);
            Cash.Instance.AddCash(cash);

            m_SearchOnce = false;
            SearchUi.gameObject.SetActive(false);

            LightOpject.SetActive(false);
            src.pitch = Random.Range(0.5f, 1.5f);
            src.Play();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (m_SearchOnce)
            {
                SearchUi.gameObject.SetActive(true);
                m_CanSearch = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SearchUi.gameObject.SetActive(false);
            m_CanSearch = false;
        }
    }
}
