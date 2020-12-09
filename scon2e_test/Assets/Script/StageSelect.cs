using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{    // Inspectorからシーン上のボタンを登録しておく
    public Button[] ButtonArray;

    void Start()
    {
        // countの値を取得
        int count = GameManager.CC;

        for (int loop = 0; loop < ButtonArray.Length; loop++)
        {
            // ボタンの有効化・無効化
            ButtonArray[loop].gameObject.SetActive(loop < count);
        }
    }
    public void StringArgFunction(string s)
    {
        SceneManager.LoadScene(s);
    }
}
