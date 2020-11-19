using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hypnAnimation : MonoBehaviour
{
    public hypnogenesis hypn;
    public int LoopCount;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hypn.hypn_animation == true)
        {
            //Debug.Log("アニメ");
            animator.SetBool("hypnflg", true);
            //animator.SetTrigger("hypn");
            hypn.hypn_animation = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > LoopCount)
        {
            //5回繰り返したら
            //enabledで停止
            animator.SetBool("hypnflg", false);
        }
    }
}
