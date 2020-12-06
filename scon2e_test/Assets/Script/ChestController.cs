using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChestController : MonoBehaviour
{
    public Animator animator;
    private PlayerController player;
    private GameObject Player;
    private GameObject exitpos;

    public bool Close = false;
    public float settime = 1.0f;
    private float CloseTime;
    public bool chestflg;

    //hideanime変数
    private float upward = 0.03f;   
    private float upward_max = 1f;
    private float upward_volume;
    private int state = 1;
    private float move_xz = 0.03f;
    private float xmove;
    private float zmove;
    private bool moveflg;
    private float time;
    private bool hideexit_flg;
    private bool openflg = false;
    private bool stopflg = false;

    public CinemachineVirtualCamera Hidecamera;
    private CinemachineComposer _composerHide;

    // Start is called before the first frame update
    void Start()
    {
        _composerHide = Hidecamera.GetCinemachineComponent<CinemachineComposer>();
        Player = GameObject.Find("Player");
        player = Player.GetComponent<PlayerController>();
        exitpos = GameObject.Find("exitpos");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hide == true)
        {
            animator.SetBool("Open", true);
            hideanime();
            hideexit_flg = true;
        }
        if(hideexit_flg == true && player.hide == false)
        {
            hideanime();
        }
    }

    private void hideanime()
    {
        Vector3 pos = Player.transform.position;
        switch (state)
        {
            //上昇
            case 1:
                pos.y += upward;
                Player.transform.position = pos;
                upward_volume += upward;
                if (upward_volume > upward_max)
                {
                    Vector3 pos2 = this.transform.position;
                    pos2.y = pos.y;
                    //目的地と自分の位置との距離
                    Vector3 dir = pos2 - pos;
                    //目的地の方を向く
                    Player.transform.rotation = Quaternion.LookRotation(dir);
                    state = 2;
                    upward_volume = 0;
                }
                break;
            //箱の座標に向かう
            case 2:
                Vector3 pos3 = this.transform.position;
                if (moveflg == false)
                {
                    xmove = (pos3.x - pos.x) * move_xz;
                    zmove = (pos3.z - pos.z) * move_xz;
                    moveflg = true;
                }

                pos.x += xmove;
                pos.z += zmove;
               
                Player.transform.position = pos;
                //宝箱の座標に近づいたら抜ける
                if (Mathf.Abs(pos3.x - pos.x) < 0.1f
                    && Mathf.Abs(pos3.z - pos.z) < 0.1f)
                {
                    state = 3;
                    moveflg = false;
                    //animator.ForceStateNormalizedTime(0f);
                }
                break;
            //宝箱に入る
            case 3:
                pos.y -= upward;
                Player.transform.position = pos;
                upward_volume += upward;
                if (upward_volume > upward_max)
                {
                    state = 4;
                    upward_volume = 0;
                }
                break;
            case 4:
                if (openflg == false)
                {
                    animator.SetBool("Open", false);
                    openflg = true;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    //animator.speed = 0f;         
                    animator.SetBool("Open", true);
                    stopflg = true;
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
                {
                    time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                }
                if (time < 1f && time > 0.25f && stopflg == true)
                {
                    //箱に隠れている
                    Hidecamera.Priority = 80;

                    chestflg = true;
                    animator.speed = 0f;
                    stopflg = false;
                }

                if (player.hide == false)
                {
                    animator.speed = 1f;
                    openflg = false;
                    state = 5;
                    Hidecamera.Priority = 10;
                    Vector3 pos4 = exitpos.transform.position;
                    pos4.y = pos.y;
                    //目的地と自分の位置との距離
                    Vector3 dir = pos4 - pos;
                    //目的地の方を向く
                    Player.transform.rotation = Quaternion.LookRotation(dir);
                }
                break;
            //箱が開いて上に行く
            case 5:
                pos.y += upward;
                Player.transform.position = pos;
                upward_volume += upward;
                if (upward_volume > upward_max)
                {                  
                    state = 6;
                    upward_volume = 0;
                }
                break;
            case 6:
                Vector3 pos5 = exitpos.transform.position;
                if (moveflg == false)
                {
                    xmove = (pos5.x - pos.x) * move_xz;
                    zmove = (pos5.z - pos.z) * move_xz;
                    moveflg = true;
                }

                pos.x += xmove;
                pos.z += zmove;

                Player.transform.position = pos;
                //宝箱の座標に近づいたら抜ける
                if (Mathf.Abs(pos5.x - pos.x) < 0.1f
                    && Mathf.Abs(pos5.z - pos.z) < 0.1f)
                {
                    animator.SetBool("Open", false);
                    moveflg = false;
                    state = 7;
                }
                break;
            case 7:
                pos.y -= upward;
                Player.transform.position = pos;
                upward_volume += upward;
                if (upward_volume > upward_max)
                {
                    hideexit_flg = false;
                    state = 1;
                    upward_volume = 0;
                    Player.GetComponent<PlayerController>().enabled = true;
                }
                break;

        }
    }
}
