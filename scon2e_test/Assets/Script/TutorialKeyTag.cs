using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKeyTag : MonoBehaviour
{
    public GameObject KeyCop;

    void OnTriggerEnter(Collider other)
    {
        KeyCop.tag = "Enemy_Key";
        GetComponent<TutorialKeyTag>().enabled = false;
    }
}
