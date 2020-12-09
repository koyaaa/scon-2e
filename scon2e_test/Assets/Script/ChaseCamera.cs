using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChaseCamera : MonoBehaviour
{

    //プレイヤーを変数に格納
    public GameObject Cent;

    //回転させるスピード
    public float rotateSpeed = 3.0f;
    public float plusPo = 3.0f;

    //追跡するカメラ
    public CinemachineVirtualCamera Rcamera;
    public CinemachineVirtualCamera Rcamera2;

    public CinemachineVirtualCamera Lcamera;
    public CinemachineVirtualCamera Lcamera2;

    //スクリーンYをとる
    private CinemachineComposer _composerR;
    private CinemachineComposer _composerR2;
    private CinemachineComposer _composerL;
    private CinemachineComposer _composerL2;
    public float screenYSpeed = 0.001f;

    //どちらのカメラが使われているのか判断する
    //true は右
    private bool CameraLR = true;

    //イラスト
    private GameObject Action;
    private RectTransform X;

    // Use this for initialization
    void Start()
    {
        _composerR = Rcamera.GetCinemachineComponent<CinemachineComposer>();
        _composerR2 = Rcamera2.GetCinemachineComponent<CinemachineComposer>();
        _composerL = Lcamera.GetCinemachineComponent<CinemachineComposer>();
        _composerL2 = Lcamera2.GetCinemachineComponent<CinemachineComposer>();

        Action = GameObject.Find("ActionUI");
        X = Action.transform.Find("BackG").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //回転させる角度
        float angle = Input.GetAxis("CameraX") * (-rotateSpeed);

        //プレイヤー位置情報
        Vector3 playerPos = Cent.transform.position;

        //カメラを回転させる
        transform.RotateAround(playerPos, Vector3.up, angle);

        //カメラの位置を補正して同期させる
        playerPos.y += plusPo;

        transform.position = playerPos;

        //Trigger
        float tri = Input.GetAxis("Trigger");
        
        if (tri < 0)
        {
            //Debug.Log("L trigger:" + tri);
            if (CameraLR)
            {
                Rcamera2.Priority = 100;
            }
            else
            {
                Lcamera2.Priority = 100;
            }
        }
        //カメラの右左変更
        else if (Input.GetButtonDown("Ybutton"))
        {
            //Debug.Log("押されたよ" + tri);
            if (CameraLR)
            {
                Rcamera.Priority = 10;
                Lcamera.Priority = 73;
                CameraLR = false;

                //Debug.Log("押されたよ");-84
                Vector2 v1 = new Vector2(-54f, -144f);
                X.anchoredPosition = v1;
            }
            else
            {
                Rcamera.Priority = 73;
                Lcamera.Priority = 10;
                CameraLR = true;
                Vector2 v1 = new Vector2(64f, -144f);
                X.anchoredPosition = v1;
            }
        }
        //else if (tri > 0)
        //{
        //    Debug.Log("R trigger:" + tri);
        //    Rcamera2.Priority = 100;
        //    Lcamera2.Priority = 10;
        //}
        else
        {
            Rcamera2.Priority = 10;
            Lcamera2.Priority = 10;
        }

        //スクリーンを触る
        float angleY = Input.GetAxis("CameraY") * (-screenYSpeed);

        _composerR.m_ScreenY -= angleY; //ScreenYを操作
        _composerR2.m_ScreenY -= angleY; //ScreenYを操作
        _composerL.m_ScreenY -= angleY; //ScreenYを操作
        _composerL2.m_ScreenY -= angleY; //ScreenYを操作
        if (_composerR.m_ScreenY > 0.8)
        {
            _composerR.m_ScreenY = 0.8f;
            _composerR2.m_ScreenY = 0.8f;
            _composerL.m_ScreenY = 0.8f;
            _composerL2.m_ScreenY = 0.8f;
        }
        if (_composerR.m_ScreenY < 0.2)
        {
            _composerR.m_ScreenY = 0.2f;
            _composerR2.m_ScreenY = 0.2f;
            _composerL.m_ScreenY = 0.2f;
            _composerL2.m_ScreenY = 0.2f;
        }
    }
}
