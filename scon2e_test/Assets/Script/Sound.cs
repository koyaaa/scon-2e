using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSE;
    public AudioClip sound01;
    public bool SearchSEflag = false;

    public AudioClip sound1;
    AudioSource audioSource;

    void Start()
    {
        audioSE = gameObject.AddComponent<AudioSource>();
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (SearchSEflag == true)
        {

            audioSE.PlayOneShot(sound01);
            SearchSEflag = false;
        }
        //mouse左が押された
        if (Input.GetMouseButtonDown(0))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
    }
}