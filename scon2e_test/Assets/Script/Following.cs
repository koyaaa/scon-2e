using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    public float distance = 1.7f;
    private GameObject player;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<OnSearchView>().keyflg == true && this.gameObject.tag == "Enemy_Key")
        {
            agent.speed = 2f;
            Vector3 pos2 = this.transform.position;
            pos2.y += 0.452f;
            //目的地と自分の位置との距離
            Vector3 dir = player.transform.position - pos2;
            //目的地の位置
            Vector3 pos = this.transform.position + dir * 1.5f;
            //目的地の方を向く
            this.transform.rotation = Quaternion.LookRotation(dir);
            //目的地を指定する
            agent.destination = pos;
            //目的地からどれくらい離れて停止するか
            agent.stoppingDistance = distance;
        }
    }
}
