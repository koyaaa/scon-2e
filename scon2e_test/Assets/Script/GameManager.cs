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
            SceneManager.LoadScene("GameClear");
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            SceneManager.LoadScene("GameClear2");
        }
        else if(SceneManager.GetActiveScene().name == "Stage3")
        {
            SceneManager.LoadScene("GameClear3");
        }
    }
}