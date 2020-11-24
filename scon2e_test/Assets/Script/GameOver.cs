using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameManager gameManager;
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //プレイヤーが当たり判定に入った時の処理
    void OnTriggerEnter(Collider CapsuleCollider)
    {
        if (CapsuleCollider.gameObject.tag == "Enemy")
        {
            gameManager.GameOver();
        }
    }
}