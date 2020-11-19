using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSE;
    public AudioClip sound01;
    public bool SearchSEflag = false;
    void Start()
    {
        audioSE = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SearchSEflag == true)
        {

            audioSE.PlayOneShot(sound01);
            SearchSEflag = false;
        }
    }
}
