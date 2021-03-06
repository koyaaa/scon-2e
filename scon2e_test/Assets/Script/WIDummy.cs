﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WIDummy : MonoBehaviour
{
    private RectTransform img; //アイコンイメージ

    private GameObject SearchUI;

    private RectTransform kagi;
    private RectTransform uzu;

    void Start()
    {
        SearchUI = transform.Find("SearechUI").gameObject;
        img = SearchUI.transform.Find("Image").GetComponent<RectTransform>();
        uzu = SearchUI.transform.Find("uzu2").GetComponent<RectTransform>();
        kagi = SearchUI.transform.Find("kagi").GetComponent<RectTransform>();
    }

    void Update()
    {
        //アイコンを表示したいワールド座標（敵キャラの足許から1.8m上空、など）
        Vector3 targetWorldPos = transform.position + Vector3.up * 1.5f;

        Vector3 scrPos = calcAnchor(targetWorldPos);
        img.anchorMin = scrPos;
        img.anchorMax = scrPos;

        Vector3 scrPos2 = calcAnchor(targetWorldPos);
        kagi.anchorMin = scrPos2;
        kagi.anchorMax = scrPos2;

        Vector3 scrPos3 = calcAnchor(targetWorldPos);
        uzu.anchorMin = scrPos3;
        uzu.anchorMax = scrPos3;
    }

    private Vector2 calcAnchor(Vector3 targetPos)
    {
       // Debug.Log("aaab");
        Vector3 anchor = Camera.main.WorldToViewportPoint(targetPos);
        if ((0 < anchor.z) && (0 <= anchor.x) && (anchor.x <= 1) &&
            (0 <= anchor.y) && (anchor.y <= 1))
        {
            //正面でかつ画角にいる場合はそのまま返す
            return anchor;
        }

        //カメラ水平面を基準にした、対象のローカル位置
        Vector3 targetCameraLocalPos =
          Camera.main.transform.InverseTransformPoint(targetPos);

        //画面右端を 0度とした反時計回りの角度
        float angleRad = Mathf.Atan2(targetCameraLocalPos.y, targetCameraLocalPos.x);

        //Cos,Sinを計算し -1～1 を 0～1 に修正
        anchor.x = 0.5f + Mathf.Cos(angleRad) * 0.45f;
        anchor.y = 0.5f + Mathf.Sin(angleRad) * 0.45f;
        return anchor;
    }
}
