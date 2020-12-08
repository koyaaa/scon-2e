using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public GameObject TutorialUI;
    public float ViewTime = 2.0f;

    //1回入ったらfalse
    private bool OneEnter = true;

    private float SaveTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (OneEnter)
        {
            //表示開始
            TutorialUI.SetActive(true);
            //UI表示開始時間
            SaveTime = Time.time;
            OneEnter = false;
        }
    }

    void Update()
    {
        if (!OneEnter)
        {
            //表示時間が2秒を超えたらオフにする
            if (SaveTime + ViewTime < Time.time)
            {
                TutorialUI.SetActive(false);
                GetComponent<Tutorial>().enabled = false;
            }
        }
    }
}
