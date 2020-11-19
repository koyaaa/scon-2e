using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [System.NonSerialized]
    public int currentStageNum = 0; //現在のステージ番号（0始まり）

    [SerializeField]
    string[] stageName; //ステージ名
    [SerializeField]
    GameObject gameClearCanvasPrefab;
    [SerializeField]
    GameObject gameOverCanvasPrefab;
    [SerializeField]
    GameObject gameClearCanvasClone;
    [SerializeField]
    GameObject gameOverCanvasClone;
    Rigidbody playerRigidbody;
    Rigidbody enemyRigidbody;

    Button[] buttons;

    //最初の処理
    void Start()
    {
        //シーンを切り替えてもこのゲームオブジェクトを削除しないようにする
        DontDestroyOnLoad(gameObject);

        //デリゲートの登録
        SceneManager.sceneLoaded += OnSceneLoaded;

        //ユーザーコントロールを取得
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    //シーンのロード時に実行（最初は実行されない）
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //改めて取得
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    //毎フレームの処理
    void Update()
    {

    }

    //次のステージに進む処理
    public void NextStage()
    {
        currentStageNum += 1;

        //コルーチンを実行
        StartCoroutine(WaitForLoadScene(currentStageNum));
    }

    //任意のステージに移動する処理
    public void MoveToStage(int stageNum)
    {
        //コルーチンを実行
        StartCoroutine(WaitForLoadScene(stageNum));

    }

    //シーンの読み込みと待機を行うコルーチン
    IEnumerator WaitForLoadScene(int stageNum)
    {
        playerRigidbody.isKinematic = true;

        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync(stageName[stageNum]);

    }

    //ゲームオーバー処理
    public void GameOver()
    {
        //キャラの移動を停止させる
        playerRigidbody.isKinematic = true;

        //ゲームオーバー画面表示
        gameOverCanvasClone = Instantiate(gameOverCanvasPrefab);

        //ボタンを取得
        buttons = gameOverCanvasClone.GetComponentsInChildren<Button>();

        //ボタンにイベント設定
        buttons[0].onClick.AddListener(Retry);
        buttons[1].onClick.AddListener(Return);

    }

    public void GameClear()
    {
        //キャラの移動を停止させる
        playerRigidbody.isKinematic = true;

        //ステージクリア画面表示
        gameClearCanvasClone = Instantiate(gameClearCanvasPrefab);

        //ボタンを取得
        buttons = gameClearCanvasClone.GetComponentsInChildren<Button>();

        //ボタンにイベント設定
        buttons[0].onClick.AddListener(Next);
        buttons[1].onClick.AddListener(Return);
    }
    
    public void Next()
    {

    }

    //リトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameOverCanvasClone);
    }

    //最初のシーンに戻る
    public void Return()
    {
        Destroy(gameOverCanvasClone);

        MoveToStage(0);
    }
}