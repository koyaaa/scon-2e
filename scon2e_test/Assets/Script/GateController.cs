using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float stp = 4.0f;

    public bool Open = false;

    private GameObject GoalAICollider;
    public GoalAI goalai;


    public float ViewTime = 2.0f;

    //1回入ったらfalse
    private bool OneEnter = true;

    private float SaveTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        GoalAICollider = GameObject.Find("GoalAICollider");
        goalai = GoalAICollider.GetComponent<GoalAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OneEnter && goalai.LockEnter)
        {
            SaveTime = Time.time;
            OneEnter = false;
        }

        if (!OneEnter)
        {
            //表示時間が2秒を超えたらオフにする
            if (SaveTime + ViewTime < Time.time)
            {
                Open = true;
            }
        }


        if (Open)
        {
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            if (pos.y <= stp)
            {
                pos.y += 0.01f;    // y座標へ0.1加算
            }

            myTransform.position = pos;  // 座標を設定
        }

       
    }
}
