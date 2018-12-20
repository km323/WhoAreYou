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

    //NextGameのイベント
    public delegate void NextGameHandler();
    public static event NextGameHandler OnNextGame;

    private void Awake()
    {
        OnNextGame = null;
    }

    private void Start()
    {
        currentState = BLACK;

        black = new List<GameObject>();
        white = new List<GameObject>();
        
        //プレイヤーの初期化
        activePlayer = Instantiate(blackPlayerPrefab);
        activePlayer.transform.position = startBlackPlayerPos[0];
        activePlayer.AddComponent<PlayerController>();

        GameObject enemy =Instantiate(whitePlayerPrefab, startWhitePlayerPos[0], whitePlayerPrefab.transform.rotation);
        enemy.GetComponent<PlayerCollision>().OnBulletHit += () => WhiteEnemyHitHandler();
    }

    //敵の当たり判定のイベント
    //private void GameOver()
    //{
    //    Debug.Log("Gameover");
    //    //StartCoroutine("CallGameEnd");
    //}
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
        if (currentState == BLACK)
        {
            activePlayer.GetComponent<PlayerCollision>().OnBulletHit += () => BlackEnemyHitHandler();
            black.Add(activePlayer);
            enemyCount = black.Count;

            //WhitePrefabで初期化
            activePlayer = Instantiate(whitePlayerPrefab,startWhitePlayerPos[white.Count % 10],whitePlayerPrefab.transform.rotation);
        }
        else
        {
            activePlayer.GetComponent<PlayerCollision>().OnBulletHit += () => WhiteEnemyHitHandler();
            white.Add(activePlayer);
            enemyCount = white.Count;

            //BlackPrefabで初期化
            activePlayer = Instantiate(blackPlayerPrefab,startBlackPlayerPos[black.Count % 10],blackPlayerPrefab.transform.rotation);
        }

        activePlayer.SetActive(false);

        currentState = -currentState;

        foreach (GameObject enemy in black)
            enemy.SetActive(false);
        foreach (GameObject enemy in white)
            enemy.SetActive(false);
    }

    private void ShowAllCharacter()
    {
        //すべての敵の初期化
        foreach (GameObject enemy in black)
            enemy.SetActive(true);
        foreach (GameObject enemy in white)
            enemy.SetActive(true);

        activePlayer.SetActive(true);
    }

    private void ActivePlayerInput()
    {
        activePlayer.AddComponent<PlayerController>();
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1f);

        Destroy(activePlayer.GetComponent<PlayerController>());
        OnNextGame();

        yield return new WaitForSeconds(1f);
        ResetGame();
        yield return null;
        ShowAllCharacter();

        yield return new WaitForSeconds(0.5f);

        ActivePlayerInput();
    }

    //IEnumerator CallGameEnd()
    //{
    //    Debug.Log("Call end");
    //    yield return new WaitForSeconds(1);

    //    SceneController.Instance.Change(Scene.Result);
    //}
}
