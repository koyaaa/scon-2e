﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject Player;
    bool hide;
    bool activef;//オブジェクト表示フラグ

    public int[] EnemyNumber = new int[20];

    void Start()
    {
        Player = GameObject.Find("Player");
        EnemyNumber[0] = -99;
    }

    void Update()
    {
        hide = playerController.hide;
        if (hide == true && activef == false)
        {
            SendHide();//追っかけているエネミーに主人公が隠れたことを知らせ

            //プレイヤーを非表示
            //Player.SetActive(false);
            activef = true;
            return;
        }
        //ごみ箱から出る(BボタンかFキー)
        if (hide == true && Input.GetButtonDown("Bbutton"))
        {
            hide = false;
            activef = false;
            Player.tag = "Player";
            Player.GetComponent<PlayerController>().enabled = true;
            playerController.hide = false;
            SendHide2();
            Color color = Player.gameObject.GetComponent<Renderer>().material.color;
            color.a = 1.0f;
            Player.gameObject.GetComponent<Renderer>().material.color = color;
            // Player.SetActive(true);
            Debug.Log("出た");
        }
    }
    void SendHide()
    {
        for (int i = 0; i < 20; i++)
        {
            switch (EnemyNumber[i])
            {
                case 1:
                    GameObject refObj;
                    refObj = GameObject.Find("Enemy1");
                    OnSearchView d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 2:
                    break;
            }
        }
    }

    //ゴミ箱からでたのをお知らせ
    void SendHide2()
    {
        for (int i = 0; i < 20; i++)
        {
            switch (EnemyNumber[i])
            {
                case 1:
                    GameObject refObj;
                    refObj = GameObject.Find("Enemy1");
                    OnSearchView d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 2:
                    break;
            }
        }
    }
}
