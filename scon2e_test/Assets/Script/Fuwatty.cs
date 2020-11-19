using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Fuwatty : MonoBehaviour
{
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する

    public GameObject target;
    public float cooltime =2.0f;
    public float chargetime = 1.0f;
    public float speed = 2.0f;
    public Color yellowColor;
    public float blink_interval;

    private float savetime;
    private bool saveflg = false;
    private bool blinkingflg = false;
    private bool accelerationflg = false;
    private float savespd = 0f;
    private float savetime2;
    private bool change_flag;
    public float acceltime;
    private float start_acceltime;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //敵が！マーク状態だったら
        if (onSearch.WANING == true)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            //前方に向かって動く
            if (accelerationflg == true)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                agent.speed = speed;
                Transform EnemyTrans = this.transform;
                EnemyTrans.Translate(Vector3.forward * speed);
                //前方に走り始めてからacceltime秒掛かったら止まる
                if(acceltime < Time.time - start_acceltime)
                {
                    GetComponent<NavMeshAgent>().isStopped = false;
                    agent.speed = savespd;
                    accelerationflg = false;
                    saveflg = false;
                }
            }
            //一回だけ現在時間を保存、accelflgを抜けたらまた入る
            if (saveflg == false)
            {
                savetime = Time.time;
                saveflg = true;
            }
            //クールタイム経過したら
            if (cooltime < Time.time - savetime && blinkingflg == false && accelerationflg == false)//クールタイム経ったら点滅
            {
                blinkingflg = true;
                savetime2 = Time.time;
            }
            //点滅
            if (blinkingflg == true)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                blinking();
                //点滅した後高速移動
                if (chargetime < Time.time - (savetime + cooltime))
                {
                    GetComponent<Renderer>().material.color = onSearch.redColor;
                    blinkingflg = false;
                    accelerationflg = true;
                    GetComponent<NavMeshAgent>().isStopped = false;
                    savespd = agent.speed;
                    start_acceltime = Time.time;
                }
            }
        }
    }

    //点滅
    private void blinking()
    {
        if(blink_interval < Time.time - savetime2)
        {
            if(change_flag == true)
            {
                GetComponent<Renderer>().material.color = onSearch.redColor;
                
            }
            else
            {
                GetComponent<Renderer>().material.color = yellowColor;
            }
            change_flag = !change_flag;
            savetime2 = Time.time;
        }
    }
}
