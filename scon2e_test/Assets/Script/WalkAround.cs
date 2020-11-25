using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class WalkAround : MonoBehaviour
{
    public OnSearchView onSearch;   //OnSearchViewスクリプトを使用する

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public GameObject target;
    public bool inArea = false;
    public float chaspeed = 0.05f;
    public Color origColor;

    public int Timemin = 0;//きょろきょろさせる
    private float SaveTime = 0;

    private Transform from = null;
    private Transform to = null;
    public float rotate_time; //振り向きの時間
    private bool rotflg;
    private float angle;
    private bool WANING_rotflg;
    public float turning_angle;
    private bool rot_direction;
    public float rotspd;
    public float chase_angle;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //GotoNextPoint();
    }

   
    void Update()
    {
        if (onSearch.WANING == true)
        {
            //Debug.Log(angle);           
            if (WANING_rotflg == true)
            {
                //加算
                if (rot_direction == true)
                {
                    this.transform.Rotate(new Vector3(0f, rotspd, 0f));
                }
                else//減算
                {
                    this.transform.Rotate(new Vector3(0f, -rotspd, 0f));
                }

                from = this.transform;
                var diff = to.position - from.position;

                var axis = Vector3.Cross(from.forward, diff);

                angle = Vector3.Angle(from.forward, diff)
                             * (axis.y < 0 ? -1 : 1);

                if (Mathf.Abs(angle) < chase_angle)
                {
                    WANING_rotflg = false;
                }
            }
            
            if(WANING_rotflg == false){            
                agent.destination = target.transform.position;//ターゲットに向かう
                GetComponent<NavMeshAgent>().isStopped = false;
                from = this.transform;
                to = target.transform;
                var diff = to.position - from.position;

                var axis = Vector3.Cross(from.forward, diff);

                angle = Vector3.Angle(from.forward, diff)
                             * (axis.y < 0 ? -1 : 1);
            }
            if (WANING_rotflg == false &&
                Mathf.Abs(angle) > turning_angle)
            {
                //Debug.Log(angle);
                if (angle > 0)
                {
                    rot_direction = true;
                }
                else
                {
                    rot_direction = false;
                }
                WANING_rotflg = true;
                GetComponent<NavMeshAgent>().isStopped = true;
            }
            return;
        }

        if (agent.remainingDistance < 0.5f && SaveTime == 0)
        {
            SaveTime = Time.time;
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        if (agent.remainingDistance < 0.5f && Time.time > SaveTime + Timemin && rotflg == false)
        {
            from = this.transform;
            to = points[destPoint].transform;
            var diff = to.position - from.position;

            var axis = Vector3.Cross(from.forward, diff);

            angle = Vector3.Angle(from.forward, diff)
                         * (axis.y < 0 ? -1 : 1);
            angle = from.transform.localEulerAngles.y + angle;
            rotflg = true;
            SaveTime = 0;
            //Debug.Log(angle);
        }

        if (target.activeInHierarchy == false)
        {
            GetComponent<Renderer>().material.color = origColor;
        }
        
        if(rotflg == true)
        {
            GetComponent<NavMeshAgent>().isStopped = true;

            // 4秒かけて、y軸を260度回転
            Hashtable hash = new Hashtable();
            hash.Add("y", angle);
            hash.Add("time", rotate_time);
            hash.Add("oncompletetarget", this.gameObject); // メソッドがあるオブジェクトを指定
            hash.Add("oncomplete", "GotoNextPoint"); // 実行するタイミング、実行するメソッド名
            iTween.RotateTo(this.gameObject, hash);
        }

    }

    void GotoNextPoint()
    {
        rotflg = false;
        GetComponent<NavMeshAgent>().isStopped = false;
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
        
    }
}
