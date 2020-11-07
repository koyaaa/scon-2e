using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class WalkAround : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public GameObject target;
    public bool inArea = false;
    public float chaspeed = 0.05f;
    public Color origColor;
  

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
