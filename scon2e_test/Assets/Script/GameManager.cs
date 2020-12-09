using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // クリア回数
    // staticにすることで新しいシーンを読み込んだ時でも削除されなくなる
    public static int CC = 0;

    //最初の処理
    void Start()
    {

    }
    //毎フレームの処理
    void Update()
    {
    }

    //ゲームオーバー処理
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        if (SceneManager.GetActiveScene().name == "ProtoStage")
        {
            if (CC >= 0)
            {
                CC = 1;
            }
            SceneManager.LoadScene("GameClear");
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            if (CC >= 1)
            {
                CC = 2;
            }
            SceneManager.LoadScene("GameClear2");
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            if (CC >= 2)
            {
                CC = 3;
            }
            SceneManager.LoadScene("GameClear3");
        }
    }
}