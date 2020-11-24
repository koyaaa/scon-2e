using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    }

    public void Next()
    {

    }
}