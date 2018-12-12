using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    //プレーヤーの初期位置　ランダムにするかこっちで指定するか
    [SerializeField]
    private Vector3[] startBlackPlayerPos;
    [SerializeField]
    private Vector3[] startWhitePlayerPos;


    //現在の自分の色
    public static readonly int BLACK = 1;
    public static readonly int WHITE = -1;

    private static int currentState = BLACK;
    public static int GetCurrentState()
    {
        return currentState;
    }

    [SerializeField]
    private GameObject blackPlayerPrefab;
    [SerializeField]
    private GameObject whitePlayerPrefab;

    //前回の自分のリスト
    private List<GameObject> black;
    private List<GameObject> white;
    
    //操作してるプレイヤー
    private GameObject activePlayer;
    private int enemyCount = 1;

    private void Start()
    {
        currentState = BLACK;

        black = new List<GameObject>();
        white = new List<GameObject>();
        
        //プレイヤーの初期化
        activePlayer = Instantiate(blackPlayerPrefab);
        activePlayer.transform.position = startBlackPlayerPos[0];
        activePlayer.AddComponent<PlayerController>();
        
        
        var enemy =Instantiate(whitePlayerPrefab, startWhitePlayerPos[0], whitePlayerPrefab.transform.rotation);
        enemy.GetComponent<PlayerCollision>().onBulletHit += () => WhiteEnemyHitHandler();
        
    }

    //敵の当たり判定のイベント
    private void WhiteEnemyHitHandler()
    {
        if (currentState == WHITE)
            return;
        
        enemyCount--;

        if (enemyCount <= 0)
            StartCoroutine("NextGame");
    }
    private void BlackEnemyHitHandler()
    {
        if (currentState == BLACK)
            return;
        
        enemyCount--;

        if (enemyCount <= 0)
            StartCoroutine("NextGame");
    }

    private void Update()
    {
    }

    private void ResetGame()
    {
        Destroy(activePlayer.GetComponent<PlayerController>());
        

        if (currentState == BLACK)
        {
            activePlayer.GetComponent<PlayerCollision>().onBulletHit += () => BlackEnemyHitHandler();
            black.Add(activePlayer);
            enemyCount = black.Count;

            //WhitePrefabで初期化
            activePlayer = Instantiate(whitePlayerPrefab,startWhitePlayerPos[white.Count],whitePlayerPrefab.transform.rotation);
        }
        else
        {
            activePlayer.GetComponent<PlayerCollision>().onBulletHit += () => WhiteEnemyHitHandler();
            white.Add(activePlayer);
            enemyCount = white.Count;

            //BlackPrefabで初期化
            activePlayer = Instantiate(blackPlayerPrefab,startBlackPlayerPos[black.Count],blackPlayerPrefab.transform.rotation);

        }

        activePlayer.SetActive(false);

        currentState = -currentState;
        
        foreach (GameObject enemy in black)
            enemy.SetActive(false);
        foreach (GameObject enemy in white)
            enemy.SetActive(false);
        
    }

    private void StartGame()
    {
        //すべての敵の初期化
        foreach (GameObject enemy in black)
            enemy.SetActive(true);
        foreach (GameObject enemy in white)
            enemy.SetActive(true);

        //プレイヤーの初期化
        activePlayer.AddComponent<PlayerController>();
        activePlayer.SetActive(true);
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1f);

        ResetGame();
        Camera.main.GetComponent<CameraController>().CameraRotate();

        yield return new WaitForSeconds(1f);

        StartGame();
    }
}
