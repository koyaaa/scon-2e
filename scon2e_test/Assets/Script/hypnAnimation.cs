using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hypnAnimation : MonoBehaviour
{
    public hypnogenesis hypn;
    public int LoopCount;
    public bool stopflg;
    public GameObject enemy_parent;
    private GameObject enemy;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > LoopCount && stopflg == true)
        {
            //5回繰り返したら
            //enabledで停止
            animator.SetBool("hypnflg", false);
            //Debug.Log("停止");
            stopflg = false;
            enemy = this.gameObject;
            enemy_parent = enemy.transform.parent.gameObject;
            enemy_parent.GetComponent<PlayerController>().enabled = true;
        }
        if (hypn.hypn_animation == true)
        {
            animator.ForceStateNormalizedTime(0f);
            //Debug.Log("アニメ");
            animator.SetBool("hypnflg", true);
            //animator.SetTrigger("hypn");
            hypn.hypn_animation = false;
            stopflg = true;
        }
    }
}
