using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hypnogenesis : MonoBehaviour
{
    public OnSearchView onSearch;

    public float ray_distance;
    public float stop_time;
    public Color blueColor;
    public Color defaultColor;
    private float save_time;
    public bool stop_flg;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //催眠する(XボタンかEキー)
        if (Input.GetButtonDown("Xbutton"))
        {
            //Rayの発射地点の座標と発射する方向の設定
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            //Rayがヒットしたら
            if (Physics.Raycast(ray, out hit, ray_distance))
            {
                //敵がヒット&&敵の色が赤でも黄色でもない
                if (hit.collider.tag == "Enemy" && onSearch.WANING == false
                    && hit.collider.GetComponent<Renderer>().material.color != onSearch.yellowColor)
                {
                    //ヒットした敵の索敵諸々のコンポーネントの停止
                    enemy = hit.collider.gameObject;
                    enemy.GetComponent<NavMeshAgent>().enabled = false;
                    enemy.GetComponent<WalkAround>().enabled = false;
                    enemy.GetComponent<Renderer>().material.color = blueColor;
                    save_time = Time.time;
                    stop_flg = true;
                }
            }
            //Debug.DrawRay(ray.origin, ray.direction * ray_distance, Color.red, 5);
        }
        //敵が機能停止してから時間が経ったら機能再開
        if(stop_time < Time.time - save_time && stop_flg == true)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemy.GetComponent<WalkAround>().enabled = true;
            enemy.GetComponent<Renderer>().material.color = defaultColor;
            stop_flg = false;
        }
    }
}
