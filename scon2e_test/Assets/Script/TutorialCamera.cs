using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialCamera : MonoBehaviour
{


    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;

    public float ViewTime = 2.0f;

    //1回入ったらfalse
    private bool OneEnter = true;

    private float SaveTime = 0f;


    void OnTriggerEnter(Collider other)
    {
        if (OneEnter)
        {
            //表示開始
            camera1.Priority = 120;
            //Camera1開始時間
            SaveTime = Time.time;
            OneEnter = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!OneEnter)
        {
            //表示時間が4秒を超えたらオフにする
            if (SaveTime + ViewTime + ViewTime < Time.time)
            {
                camera2.Priority = 10;
                GetComponent<TutorialCamera>().enabled = false;
            }//表示時間が2秒を超えたらCamera2にする
            else if (SaveTime + ViewTime < Time.time)
            {
                camera1.Priority = 10;
                camera2.Priority = 120;
            }
        }
    }
}
