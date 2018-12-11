using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    [SerializeField]
    private Vector3[] startBlackPlayerPos;
    [SerializeField]
    private Vector3[] startWhitePlayerPos;

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

    private List<GameObject> black;
    private List<GameObject> white;

    //操作しているキャラ
    private GameObject activePlayer;
    private int enemyCount = 1;

    private void Start()
    {
        currentState = BLACK;

        black = new List<GameObject>();
        white = new List<GameObject>();


        //初期プレイヤー作成
        activePlayer = Instantiate(blackPlayerPrefab);

        activePlayer.transform.position = startBlackPlayerPos[0];
        activePlayer.AddComponent<PlayerController>();


        //最初敵の生成　こいつはListに入れない予定
        var enemy =Instantiate(whitePlayerPrefab, startWhitePlayerPos[0], whitePlayerPrefab.transform.rotation);
        enemy.GetComponent<PlayerCollision>().onBulletHit += () => WhiteEnemyHitHandler();
        //white.Add(enemy);
    }

    //これをイベント化してPlayerにlistにAddする前に埋め込む予定
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

    private void Update()//UpdateにはPouseくらいしか書かない予定
    {
    }

    private void ResetGame()
    {
        Destroy(activePlayer.GetComponent<PlayerController>());

        if (currentState == BLACK)
        {
            activePlayer.GetComponent<PlayerCollision>().onBulletHit += () => BlackEnemyHitHandler();
            black.Add(activePlayer);
            activePlayer = Instantiate(whitePlayerPrefab,startWhitePlayerPos[white.Count],whitePlayerPrefab.transform.rotation);
            enemyCount = black.Count;
        }
        else
        {
            activePlayer.GetComponent<PlayerCollision>().onBulletHit += () => WhiteEnemyHitHandler();
            white.Add(activePlayer);
            activePlayer = Instantiate(blackPlayerPrefab,startBlackPlayerPos[black.Count],blackPlayerPrefab.transform.rotation);
            enemyCount = white.Count;
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

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1f);

        ResetGame();
        Camera.main.GetComponent<CameraController>().CameraRotate();

        yield return new WaitForSeconds(1f);

        StartGame();
    }
}
