using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSE;

    public AudioClip sound01;
    public bool SearchSEflag = false; //発見(!)音

    public AudioClip sound02;
    public bool WhoSEflag = false; //誰(?)音

    public AudioClip sound03;
    public bool HypnoSEflag = false;  //催眠音

    public AudioClip sound04;
    public bool HideSEflag = false;  //隠れる音

    public AudioClip sound05;
    public bool GateSEflag = false;  //ドアを開ける音

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
        //発見(!)音
        if (SearchSEflag == true)
        {
           
            audioSE.PlayOneShot(sound01);
            SearchSEflag = false;
        }

        //誰(?)音
        if (WhoSEflag == true)
        {
            
            audioSE.PlayOneShot(sound02);
            WhoSEflag = false;
        }

        //催眠音
        if (HypnoSEflag == true)
        {
            
            audioSE.PlayOneShot(sound03);
            HypnoSEflag = false;
        }


        if (HideSEflag == true)
        {
            
            audioSE.PlayOneShot(sound04);
            HideSEflag = false;
        }

        if (GateSEflag == true)
        {

            audioSE.PlayOneShot(sound05);
            GateSEflag = false;
        }

        //mouse左が押された
        if (Input.GetMouseButtonDown(0))
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
    }
}