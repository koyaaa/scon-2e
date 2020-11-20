using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameManager gameManager;
    public OnSearchView onSearch;

    //public bool GameOverFlag = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //プレイヤーが当たり判定に入った時の処理
    void OnTriggerEnter(Collider CapsuleCollider)
    {
        GameObject enemy = CapsuleCollider.transform.parent.gameObject;
        onSearch = enemy.GetComponent<OnSearchView>();
        if (CapsuleCollider.gameObject.tag == "Enemy" && onSearch.WANING == true)
        { 
            gameManager.GameOver();
        }
    }
}