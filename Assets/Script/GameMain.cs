using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    //private List<GameObject> players;

    private List<GameObject> black;
    private List<GameObject> white;

    //操作しているキャラ
    private GameObject activePlayer;

    [SerializeField]
    private GameObject blackPlayerPrefab;

    [SerializeField]
    private GameObject whitePlayerPrefab;

    [SerializeField]
    private Vector3 startPlayerPos;

    [SerializeField]
    private Vector3 startEnemyPos;

    private int enemyCount = 1;


    const int BLACK = 1;
    const int WHITE = -1;

    public int currentState=BLACK;

    private void Start()
    {
        //players = new List<GameObject>();

        currentState = BLACK;

        black = new List<GameObject>();
        white = new List<GameObject>();


        //初期プレイヤー作成
        activePlayer = Instantiate(blackPlayerPrefab);

        activePlayer.transform.position = startPlayerPos;
        activePlayer.AddComponent<PlayerController>();


        //最初敵の生成　こいつはListに入れない
        var enemy =Instantiate(whitePlayerPrefab, startEnemyPos, whitePlayerPrefab.transform.rotation);
        white.Add(enemy);
    }

    //これをイベント化してPlayerにlistにAddする前に埋め込む予定
    public void WhiteEnemyHitHandler()
    {
        if (currentState == WHITE)
            return;
        
        foreach (GameObject enemy in white)
        {
            if ( enemy.activeSelf)
            {
                enemy.SetActive(false);
                break;
            }
        }
        enemyCount--;

        if (enemyCount <= 0)
            ResetGame();
    }
    public void BlackEnemyHitHandler()
    {
        if (currentState == BLACK)
            return;
        
        foreach (GameObject enemy in black)
        {
            if (enemy.activeSelf)
            {
                enemy.SetActive(false);
                break;
            }
        }

        enemyCount--;

        if (enemyCount <= 0)
            ResetGame();
    }

    private void Update()//UpdateにはPouseくらいしか書かない予定
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            WhiteEnemyHitHandler();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            BlackEnemyHitHandler();
        }
    }

    private void ResetGame()
    {
        activePlayer.SetActive(false);
        Destroy(activePlayer.GetComponent<PlayerController>());

        //ここでPlayerのTrigerで呼ばれるイベントを追加したい
        //そのあとに敵として保存する
        

        if (currentState == BLACK)
        {
            black.Add(activePlayer);
            activePlayer = Instantiate(whitePlayerPrefab);
            enemyCount = white.Count;
        }
        else
        {
            white.Add(activePlayer);
            activePlayer = Instantiate(blackPlayerPrefab);
            enemyCount = black.Count;
        }

        activePlayer.SetActive(false);

        currentState = -currentState;

        foreach (GameObject enemy in black)
            enemy.SetActive(false);
        foreach (GameObject enemy in white)
            enemy.SetActive(false);

        //Camera.main.GetComponent<CameraController>().CameraRotate();
    }

    private void StartGame()//回転or反転が終わったら呼ぶ
    {
        //PlayerのOnEnableでRecordPlayをここで呼ぶ
        foreach (GameObject enemy in black)
            enemy.SetActive(true);
        foreach (GameObject enemy in white)
            enemy.SetActive(true);

        
        //PlayerControllerにOnTriggerで終了判定をつける予定
        activePlayer.AddComponent<PlayerController>();

        activePlayer.SetActive(true);
    }
}
