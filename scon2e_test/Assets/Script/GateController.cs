using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float stp = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        if (pos.y <= stp)
        {
            pos.y += 0.1f;    // y座標へ0.1加算
        }

        myTransform.position = pos;  // 座標を設定


       
    }
}
