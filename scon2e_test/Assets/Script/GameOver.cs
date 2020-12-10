using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameManager gameManager;
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する
    public float ray_distance = 0.5f;

    private float step_time;    // 経過時間カウント用


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        step_time = 0.0f;       // 経過時間初期化
    }

    void Update()
    {
        onSearch = this.GetComponent<OnSearchView>();
        if (onSearch.WANING == true)
        {
            Vector3 pos = this.transform.position;
            pos.y += 0.5f;    // y座標へ0.5加算
            //Rayの発射地点の座標と発射する方向の設定
            Ray ray = new Ray(pos, this.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, ray_distance))
            {
                //ヒットしたものがプレイヤー
                if (hit.collider.tag == "Player")
                {
                    // 経過時間をカウント
                    step_time += Time.deltaTime;

                    // 1秒後に画面遷移（sceneへ移動）
                    if (step_time >= 0.3f)
                    {
                        gameManager.GameOver();
                    }

                }
            }
            Debug.DrawRay(ray.origin, ray.direction * ray_distance, Color.red, 5);
        }

    }
}