using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject Player;
    bool hide;
    bool activef;//オブジェクト表示フラグ

    public int EnemyMax = 0;

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
        if (hide == true && Input.GetButtonDown("Bbutton")
            /*&& Chest.GetComponent<ChestController>().chestflg == true*/)
        {
            GameObject ch = playerController.Chest;
            if (ch.GetComponent<ChestController>().chestflg == true)
            {
                ch.GetComponent<ChestController>().chestflg = false;
                hide = false;
                activef = false;
                Player.tag = "Player";
                //Player.GetComponent<PlayerController>().enabled = true;
                playerController.hide = false;
                SendHide2();

                Color color = Player.gameObject.GetComponent<Renderer>().material.color;
                color.a = 1.0f;
                Player.gameObject.GetComponent<Renderer>().material.color = color;



                Color color2 = Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
                color2.a = 1.0f;
                Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = color2;

                // Player.SetActive(true);
                Debug.Log("出た");
            }
        }
    }
    void SendHide()
    {
        for (int i = 1; i <= EnemyMax; i++)
        {
            switch (i)
            {
                case 1:
                    GameObject refObj;
                    refObj = GameObject.Find("Cop");
                    OnSearchView d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 2:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (1)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 3:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (2)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 4:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (3)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 5:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (4)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 6:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (5)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 7:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (6)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
                case 8:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (7)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = true;
                    break;
            }
        }
    }

    //ゴミ箱からでたのをお知らせ
    void SendHide2()
    {
        for (int i = 1; i <= EnemyMax; i++)
        {
            switch (/*EnemyNumber[i]*/i)
            {
                case 1:
                    GameObject refObj;
                    refObj = GameObject.Find("Cop");
                    OnSearchView d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 2:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (1)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 3:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (2)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 4:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (3)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 5:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (4)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 6:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (5)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 7:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (6)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
                case 8:
                    //GameObject refObj;
                    refObj = GameObject.Find("Cop (7)");
                    d2 = refObj.GetComponent<OnSearchView>();
                    d2.HideSearch = false;
                    break;
            }
        }
    }
}
