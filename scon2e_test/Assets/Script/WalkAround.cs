using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkAround : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public GameObject target;
    private bool inArea = false;
    public float chaspeed = 0.05f;
    public Color origColor;

    //public GameObject muzzlePoint;
    //public GameObject ball;
    //public float speed = 30f;
    //private int attackTime = 0;
    //public int intvalTime = 30;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //GotoNextPoint();
    }

   
    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            
            GotoNextPoint();
        }

        if (target.activeInHierarchy == false)
        {
            GetComponent<Renderer>().material.color = origColor;
        }

        if (inArea == true)
        {
            /*attackTime += 1;
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * 30.0f);
            if (attackTime % intvalTime == 0)
            {
                EneCannonShot();
            }*/
        }
    }

    void GotoNextPoint()
    { 
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    /*public void EneCannonShot()
    {
        if (target.activeInHierarchy == false)
        {
            GetComponent<Renderer>().material.color = origColor;
            inArea = false;
        }

        Vector3 mballPos = muzzlePoint.transform.position;
        GameObject newBall = Instantiate(ball, mballPos, transform.rotation);
        Vector3 dir = muzzlePoint.transform.forward;

        newBall.GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.Impulse);
        newBall.name = ball.name;
        Destroy(newBall, 0.8f);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            inArea = true;
            GetComponent<Renderer>().material.color = new Color(255f / 255f, 65f / 255f, 26f / 255f, 255f / 255f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            inArea = false;
            GetComponent<Renderer>().material.color = origColor;
            chaspeed = 0;
            //GotoNextPoint();
        }
    }

}
