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
    private Rigidbody rB;
    private Vector3 rbVelo;

    //    public Text goalText;
    public bool goalOn;
    public ParticleSystem explosion;
   // public ParticleSystem light;
    public Text goalText;
    public GameObject goalButton;
    public Text failText;
    public GameObject failButton;
    private Vector3 height;
    public float jumpForce = 20.0f;
    private float jumpTime = 0.0f;
    public float turboForce = 2.0f;

    Vector3 moveDirection;
    public float moveTurnSpeed = 10f;

    //カメラ移動用
    public CinemachineVirtualCamera vCamera;

    public bool ReverseKey;
    //true:ごみ箱に隠れてるfalse:隠れてない
    public bool hide;
    //Camera camera;


    void Start()
    {
        rB = GetComponent<Rigidbody>();
        goalText.enabled = false;
        goalButton.SetActive(false);
        goalOn = false;
        failText.enabled = false;
        failButton.SetActive(false);
        hide = false;
       // ReverseKey = false;

       // Debug.Log("button0");
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
                transform.rotation = Quaternion.LookRotation(moveForward);
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

        height = this.GetComponent<Transform>().position;
        if (height.y <= -3.0f)
        {
            explosion.transform.position = this.transform.position;
            explosion.Play();
            this.gameObject.SetActive(false);
            failText.enabled = true;
            failButton.SetActive(true);
        }


        //if (height.y >= 3f)
        //{
        //    jumpTime = 10f;
        //}

        /*if (jumpTime > 1f)
        {
            jumpTime--;
        }
        else*/ if (height.y <= 0f)
        {
            jumpTime = 60f;
        }
        else if (height.y <= 0.5f)
        {
            jumpTime = 0f;
        }


        
        if (jumpTime == 0 && (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 1")))
        {
            rB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            jumpTime = 60f;
        }

    }

    //プレイヤー前方のボックスコライダー
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
                this.tag = "HidePlayer";
                GetComponent<PlayerController>().enabled = false;
            }
        }
    }

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
