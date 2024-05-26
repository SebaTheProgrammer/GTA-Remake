using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressE : MonoBehaviour
{
    [SerializeField]
    private AudioSource src;

    [SerializeField]
    private AudioClip effect;

    [SerializeField]
    private string WichButton;

    void Start()
    {
        src.clip = effect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(WichButton))
        {
            src.pitch = Random.Range(0.8f, 1.2f);
            src.Play();
        }
    }
}
