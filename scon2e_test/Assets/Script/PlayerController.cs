using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12.0f;
    public float brake = 0.5f;
    private Animator animator;
    private Rigidbody rB;
    private Vector3 rbVelo;

    //    public Text goalText;
    public bool goalOn;
   // public ParticleSystem explosion;
   // public ParticleSystem light;
    //public Text goalText;
    //public GameObject goalButton;
    //public Text failText;
    //public GameObject failButton;
   // private Vector3 height;
    //public float jumpForce = 20.0f;
    //private float jumpTime = 0.0f;
    public float turboForce = 2.0f;
    public float ray_distance;

    Vector3 moveDirection;
    public float moveTurnSpeed = 10f;

    //カメラ移動用
    public CinemachineVirtualCamera vCamera;

    public bool ReverseKey;
    //true:ごみ箱に隠れてるfalse:隠れてない
    public bool hide;
    public bool hide2 = false;
    //Camera camera;
    public float angleSpeed = 4.0f;

    //隠れたチェスト
    public GameObject Chest;

    private float m_MaxDistance;
    private float m_Speed;
    private bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;
    LayerMask mask;

    private bool doorflg;
    private GameObject door;

    void Start()
    {
        Application.targetFrameRate = 60;
        rB = GetComponent<Rigidbody>();
        //goalText.enabled = false;
        //goalButton.SetActive(false);
        goalOn = false;
        //failText.enabled = false;
        //failButton.SetActive(false);
        hide = false;
        // ReverseKey = false;

        // Debug.Log("button0");

        m_MaxDistance = 3f;
        m_Speed = 20.0f;
        m_Collider = GetComponent<Collider>();
        mask = LayerMask.GetMask("Door");
    }

    //void Update()
    //{
    //   camera = Camera.main;

    //}

    void FixedUpdate()
    {
        if (goalOn == false)
        {
            rbVelo = Vector3.zero;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            if (ReverseKey == false)
            {
                x = -x;
                z = -z;
            }

            //    var input = new Vector3(x, 0f, z);


            //moveDirection = new Vector3(x * speed, 0, z * speed);

            //if (moveDirection.magnitude > 0.01f && !(Input.GetKey(KeyCode.LeftShift)))
            //{

            //    //　押した方向をカメラの向きに合わせて変換
            //    var convertInputToCameraDirection = Quaternion.FromToRotation(moveDirection, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            //    //　ユニティちゃんの角度をカメラの方向に合わせた角度のY値分だけ回転させる
            //    transform.rotation = Quaternion.AngleAxis(convertInputToCameraDirection.eulerAngles.y, Vector3.up);


            //    //Quaternion moveRot = Quaternion.LookRotation(moveDirection);
            //    //transform.rotation = Quaternion.Slerp(transform.rotation, moveRot, Time.deltaTime * moveTurnSpeed);
            //}
            //rbVelo = rB.velocity;
            //rB.AddForce(x * speed - rbVelo.x * brake, 0, z * speed - rbVelo.z * brake, ForceMode.Impulse);

            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // 方向キーの入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rB.velocity = moveForward * speed + new Vector3(0, rB.velocity.y, 0);

            // キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                Quaternion q = Quaternion.LookRotation(moveForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * angleSpeed);
            }

            if (rbVelo.z < 0)
            {
                vCamera.Priority = 100;
            }
            else if (rbVelo.z > 0)
            {
                vCamera.Priority = 5;
            }
        }

        //height = this.GetComponent<Transform>().position;
        //if (height.y <= -3.0f)
        //{
        //    explosion.transform.position = this.transform.position;
        //    explosion.Play();
        //    this.gameObject.SetActive(false);
        //    failText.enabled = true;
        //    failButton.SetActive(true);
        //}


        //if (height.y >= 3f)
        //{
        //    jumpTime = 10f;
        //}

        /*if (jumpTime > 1f)
        {
            jumpTime--;
        }
        ////else if (height.y <= 0f)
        ////{
        //    jumpTime = 60f;
        //}
        //else if (height.y <= 0.5f)
        //{
        //    jumpTime = 0f;
        //}


        
        //if (jumpTime == 0 && (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 1")))
        //{
        //    rB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        //    jumpTime = 60f;
        //}
        */
    }

    void Update()
    {
        //ごみ箱に隠れる(BボタンかFキー)
        if (hide == false && hide2 == false && Input.GetButtonDown("Bbutton"))
        {
            //Rayの発射地点の座標と発射する方向の設定
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, ray_distance))
            {
                //ヒットしたものがごみ箱
                if (hit.collider.tag == "Gomibako")
                {
                    hide = true;
                    hide2 = true;
                    //gameObject.SetActive(false);
                    Debug.Log("隠れた");

                    Color color = gameObject.GetComponent<Renderer>().material.color;
                    color.a = 0.0f;
                    gameObject.GetComponent<Renderer>().material.color = color;

                    Color color2 = transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
                    color2.a = 0.0f;
                    transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = color2;

                    this.tag = "HidePlayer";
                    GetComponent<PlayerController>().enabled = false;
                    rB.velocity = Vector3.zero;
                    rB.angularVelocity = Vector3.zero;

                    Chest = hit.collider.gameObject;
                    Chest.GetComponent<ChestController>().hideflg = true;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * ray_distance, Color.red, 5);
        }
        else if (hide == false && hide2 == true)
        {
            //hide2 = true;
            hide2 = false;
        }

        //ドアを開ける
        if (Input.GetButtonDown("Bbutton"))
        {
            //m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale / 2, transform.forward, out m_Hit, transform.rotation, ray_distance, mask);
            //Collider[] hitColliders = Physics.OverlapBox(this.transform.position, this.transform.localScale, transform.rotation, mask);
            //if (m_HitDetect || hitColliders.Length > 0)
            //{

            //Rayの発射地点の座標と発射する方向の設定
            Ray ray2 = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 2.0f, mask)){
                /*if (m_HitDetect)
                {
                    door = m_Hit.collider.gameObject;
                }
                else
                {
                    door = hitColliders[0].gameObject;
                }*/
                door = hit2.collider.gameObject;
                animator = door.GetComponent<Animator>();
                if (doorflg == false)
                { 
                    animator.SetBool("Open", true);
                    doorflg = true;
                }
                else
                {
                    animator.SetBool("Open", false);
                    doorflg = false;
                }
            }
        }
    }
    /*//プレイヤー前方のボックスコライダー
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Gomibako")
        {
            //ごみ箱に隠れる(BボタンかFキー)
            if (hide == false && Input.GetButtonDown("Bbutton"))
            {
                hide = true;
                //gameObject.SetActive(false);
                Debug.Log("隠れた");

                Color color = gameObject.GetComponent<Renderer>().material.color;
                color.a = 0.0f;
                gameObject.GetComponent<Renderer>().material.color = color;

                Color color2 = transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
                color2.a = 0.0f;
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = color2;

                this.tag = "HidePlayer";
                GetComponent<PlayerController>().enabled = false;
                rB.velocity = Vector3.zero;
                rB.angularVelocity = Vector3.zero;
            }
        }
    }*/

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            //other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            rB.AddForce(-rbVelo.x * 0.8f, 0, -rbVelo.z * 0.8f, ForceMode.Impulse);
            goalText.enabled = true;
            goalButton.SetActive(true);
            goalOn = true;
          // light.Play();
        }
        if (other.gameObject.tag == "Jump")
        {
            rB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Turbo")
        {
            Vector3 vel = rB.velocity;
            rB.AddForce(vel.x * turboForce, 0, vel.z * turboForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet")
        {
            explosion.transform.position = this.transform.position;
            explosion.Play();
            this.gameObject.SetActive(false);
            failText.enabled = true;

        }
        if (other.gameObject.tag == "Bounce")
        {
            StartCoroutine("WaitKeyInput");
        }
    }

    IEnumerator WaitKeyInput()
    {
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        {
            yield return new WaitForSeconds(1.0f);
        }
        this.gameObject.GetComponent<PlayerController>().enabled = true;
    }*/
}
