using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Fuwatty : MonoBehaviour
{
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する
    //public WalkAround walk;   //WalkAroundスクリプトを使用する
    public GameObject pos;
    //public GameObject player;

    public GameObject target;
    public float cooltime =2.0f;
    public float chargetime = 1.0f;
    public float speed = 2.0f;
    public Color yellowColor;

    private float savetime;
    private bool saveflg = false;
    private bool blinkingflg = false;
    private bool accelerationflg = false;
    private bool posflg = false;
    private float savespd = 0f;
    private int cnt;
    public float acceltime;
    private float kari;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pos = GameObject.Find("pos");
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<NavMeshAgent>().isStopped);
        if (onSearch.WANING == true)//敵が！マーク状態だったら
        {
            /*if (posflg == true)
            {
                Vector3 tpos = target.transform.position - pos;
                Vector3 pos = target.transform.position;
            }*/
            GetComponent<NavMeshAgent>().isStopped = false;//navmesh有効化
            if (accelerationflg == true)
            {
                agent.speed = speed;

                agent.destination = pos.transform.position;
                //walk.target = pos;
                //if (agent.remainingDistance < 0.5f)
                if(acceltime < Time.time - kari)
                {
                    Debug.Log("あああ");
                    agent.speed = savespd;
                    accelerationflg = false;
                    //walk.target = player;
                    saveflg = false;
                }
            }
            else
            {
              agent.destination = target.transform.position;//ターゲットに向かう
            }
            if (saveflg == false)//一回だけ現在時間を保存
            {
                savetime = Time.time;
                saveflg = true;
            }
            if (cooltime < Time.time - savetime && blinkingflg == false && accelerationflg == false)//クールタイム経ったら点滅
            {
                blinkingflg = true;
                pos.transform.position = target.transform.position;
                //posflg = true;
                //saveflg = false;
            }
            if (blinkingflg == true)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                blinking();
                if (chargetime < Time.time - (savetime + cooltime))
                {
                    GetComponent<Renderer>().material.color = onSearch.redColor;
                    blinkingflg = false;
                    accelerationflg = true;
                    GetComponent<NavMeshAgent>().isStopped = false;
                    cnt = 0;
                    savespd = agent.speed;
                    kari = Time.time;
                }
            }
        }
    }

    //点滅
    private void blinking()
    {
        if (cnt % 60 == 0)
        {
            GetComponent<Renderer>().material.color = onSearch.redColor;
        }
        if(cnt++ % 60 == 30)
        {
            GetComponent<Renderer>().material.color = yellowColor;
        }

    }
}
