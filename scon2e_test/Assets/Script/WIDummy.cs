using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WIDummy : MonoBehaviour
{
    public Image img; //アイコンイメージ

    void Update()
    {
        //アイコンを表示したいワールド座標（敵キャラの足許から1.8m上空、など）
        Vector3 targetWorldPos = transform.position + Vector3.up * 1.5f;

        Vector3 scrPos = calcAnchor(targetWorldPos);
        img.rectTransform.anchorMin = scrPos;
        img.rectTransform.anchorMax = scrPos;
    }

    private Vector2 calcAnchor(Vector3 targetPos)
    {
        Debug.Log("aaab");
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
