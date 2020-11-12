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

    
    



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //GotoNextPoint();
    }

   
    void Update()
    {
        if (agent.remainingDistance < 0.5f && SaveTime == 0)
        {
            SaveTime = Time.time;
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        if (agent.remainingDistance < 0.5f && Time.time > SaveTime + Timemin)
        {
            //Debug.Log("次へ");
            GetComponent<NavMeshAgent>().isStopped = false;
            GotoNextPoint();
            SaveTime = 0;
        }

        if (target.activeInHierarchy == false)
        {
            GetComponent<Renderer>().material.color = origColor;
        }
        
        if(onSearch.WANING == true)
        {
            //Debug.Log("はいった");
            agent.destination = target.transform.position;//ターゲットに向かう
        }
        
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
   
}
