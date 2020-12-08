using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hypnogenesis : MonoBehaviour
{
    public OnSearchView onSearch;
    public hypnAnimation hypn;
    public GameObject enemy_parent;

    public float ray_distance;
    public float stop_time;
    public Color blueColor;
    public Color defaultColor;
    public bool hypn_animation;
    private float save_time;
    private float save_time2;
    private GameObject enemy;
    private Rigidbody rB;
    private bool hypnflg;
    public GameObject soundObj;
    public Sound soundManager;

    private float m_MaxDistance;
    private float m_Speed;
    private bool m_HitDetect;
    private bool hitflg;

    Collider m_Collider;
    RaycastHit m_Hit;
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        soundObj = GameObject.Find("Soundmanager");
        soundManager = soundObj.GetComponent<Sound>();
        //nullエラー出るから適当に入れる
        enemy_parent = GameObject.Find("Cop");
        enemy = GameObject.Find("Cop");
        m_MaxDistance = 300.0f;
        m_Speed = 20.0f;
        m_Collider = GetComponent<Collider>();
        mask = LayerMask.GetMask("Search");
    }

    // Update is called once per frame
    void Update()
    {
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, ray_distance);
   
        //RayBoxが敵にヒット
        if (m_HitDetect && m_Hit.collider.tag == "EnemyHit")
        {
            hitflg = true;
            enemy = m_Hit.collider.gameObject;
            enemy_parent = enemy.transform.parent.gameObject;
        }
        //RayBoxの出る場所の判定
        Collider[] hitColliders = Physics.OverlapBox(this.transform.position, this.transform.localScale, transform.rotation, mask);
        if (hitColliders.Length > 0)
        {
            hitflg = true;
            enemy = hitColliders[0].gameObject;
        }

        //催眠する(XボタンかEキー)
        if (Input.GetButtonDown("Xbutton") && hypnflg == false)
        {
            //催眠音
            soundManager.HypnoSEflag = true;

            //催眠中プレイヤーの硬直
            this.GetComponent<PlayerController>().enabled = false;
            rB.velocity = Vector3.zero;
            rB.angularVelocity = Vector3.zero;

            save_time2 = Time.time;
            //催眠エフェクトフラグ
            hypn_animation = true;

            if(hitflg == true)
            {
                onSearch = enemy_parent.GetComponent<OnSearchView>();
                if (enemy_parent.GetComponent<Renderer>().material.color != onSearch.yellowColor && onSearch.WANING == false
                    && onSearch.hypnflg == false)
                {
                    //ヒットした敵の索敵諸々のコンポーネントの停止
                    enemy_parent.GetComponent<NavMeshAgent>().enabled = false;
                    enemy_parent.GetComponent<WalkAround>().enabled = false;
                    enemy_parent.GetComponent<Renderer>().material.color = blueColor;
                    save_time = Time.time;
                    onSearch.hypnflg = true;
                }
                //鍵を持ってる敵だったら
                if (enemy_parent.gameObject.tag == "Enemy_Key")
                {
                    onSearch.keyflg = true;
                    enemy_parent.GetComponent<NavMeshAgent>().enabled = true;
                }
                hitflg = false;
            }
            onSearch.uzuflg = true;
            hypnflg = true;
        }

        //催眠モーションが終わったら動ける
        if (hypn.stopflg == false)
        {
            hypnflg = false;
        }
        //敵が機能停止してから時間が経ったら機能再開
        if (stop_time < Time.time - save_time && onSearch.hypnflg == true && enemy_parent.gameObject.tag != "Enemy_Key")
        {
            enemy_parent.GetComponent<NavMeshAgent>().enabled = true;
            enemy_parent.GetComponent<WalkAround>().enabled = true;
            enemy_parent.GetComponent<Renderer>().material.color = defaultColor;
            onSearch.hypnflg = false;
            onSearch.uzuflg = false;
        }
        if (onSearch.hypnflg == true)
        {
            onSearch.uzumetor = ((float)Time.time - save_time) / (float)stop_time;
            //Debug.Log(onSearch.uzumetor);            
        }
    }

    //sceneviewでrayboxを表示
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * ray_distance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * ray_distance, transform.localScale);
        }
        //Physics.CheckBox
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
