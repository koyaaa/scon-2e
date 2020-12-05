//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChestController : MonoBehaviour
//{
//    public Animator animator;
//    public PlayerController playerController;   //PlayerControllerScriptスクリプトを使用する

//    public bool Close = false;
//    public float settime = 1.0f;
//    private float CloseTime;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (playerController.hide == true)
//        {
//            animator.SetBool("Open", true);
//            Close = true;
//        }

//        if (Close == true)
//        {
//            CloseTime = Time.time;
//            Debug.Log("Set");
//            Close = false;
//        }

//        if (settime < Time.time - CloseTime)
//        {
//            animator.SetBool("Open", false);
//            Debug.Log("Close");
//        }
//    }
//}
