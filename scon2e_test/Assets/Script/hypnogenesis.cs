using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hypnogenesis : MonoBehaviour
{
    public OnSearchView onSearch;
    public GameObject enemy_parent;

    public float ray_distance;
    public float stop_time;
    public Color blueColor;
    public Color defaultColor;
    public bool hypn_animation;
    public float rigor_time;
    private float save_time;
    private float save_time2;
    private GameObject enemy;
    private Rigidbody rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //催眠する(XボタンかEキー)
        if (Input.GetButtonDown("Xbutton"))
        {
            //催眠中プレイヤーの硬直
            this.GetComponent<PlayerController>().enabled = false;
            rB.velocity = Vector3.zero;
            rB.angularVelocity = Vector3.zero;

            save_time2 = Time.time;
            //催眠エフェクトフラグ
            hypn_animation = true;
            //Rayの発射地点の座標と発射する方向の設定
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            //Rayがヒットしたら
            if (Physics.Raycast(ray, out hit, ray_distance))
            {
                //敵がヒット&&敵の色が赤でも黄色でもない
                if (hit.collider.tag == "EnemyHit" && onSearch.WANING == false
                    /*&& hit.collider.GetComponent<Renderer>().material.color != onSearch.yellowColor*/)
                {
                    
                    enemy = hit.collider.gameObject;
                    enemy_parent = enemy.transform.parent.gameObject;
                    if (enemy_parent.GetComponent<Renderer>().material.color != onSearch.yellowColor) {
                        //ヒットした敵の索敵諸々のコンポーネントの停止
                        enemy_parent.GetComponent<NavMeshAgent>().enabled = false;
                        enemy_parent.GetComponent<WalkAround>().enabled = false;
                        enemy_parent.GetComponent<Renderer>().material.color = blueColor;
                        save_time = Time.time;
                        onSearch.hypnflg = true;
                    }
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * ray_distance, Color.red, 5);
        }
        //敵が機能停止してから時間が経ったら機能再開
        if (stop_time < Time.time - save_time && onSearch.hypnflg == true)
        {
            enemy_parent.GetComponent<NavMeshAgent>().enabled = true;
            enemy_parent.GetComponent<WalkAround>().enabled = true;
            enemy_parent.GetComponent<Renderer>().material.color = defaultColor;
            onSearch.hypnflg = false;
        }

        //催眠モーションが終わったら動ける
        if(rigor_time < Time.time - save_time2)
        {
            this.GetComponent<PlayerController>().enabled = true;
        }
    }
}
