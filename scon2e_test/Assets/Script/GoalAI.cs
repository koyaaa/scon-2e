using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAI : MonoBehaviour
{
    public GameObject KeyCop;
    public OnSearchView onSearch;

    public bool LockEnter = false;

    void Start()
    {
        onSearch = KeyCop.GetComponent<OnSearchView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (onSearch.keyflg)
        {
            LockEnter = true;
        }
    }
}
