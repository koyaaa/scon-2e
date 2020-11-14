using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject Player;
    bool hide;
    bool activef;//オブジェクト表示フラグ

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        hide = playerController.hide;
        if (hide == true && activef == false)
        {
            //プレイヤーを非表示
            Player.SetActive(false);
            activef = true;
            return;
        }
        //ごみ箱から出る(BボタンかFキー)
        if (hide == true && Input.GetButtonDown("Bbutton"))
        {
            hide = false;
            activef = false;
            playerController.hide = false;
            Player.SetActive(true);
            Debug.Log("出た");
        }
    }

}
