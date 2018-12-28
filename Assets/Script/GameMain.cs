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

    private int turn = 0;
    public int GetTurn()
    {
        return turn;
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
    public static NextGameHandler OnNextGame;

    private StageManager stageManager;
    private CameraEffect cameraEffect;

    private void Awake()
    {
        OnNextGame = null;
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        cameraEffect = new CameraEffect();
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
            activePlayer = Instantiate(whitePlayerPrefab,startWhitePlayerPos[turn % 10],whitePlayerPrefab.transform.rotation);
        }
        else
        {
            activePlayer.GetComponent<PlayerCollision>().OnBulletHit += () => WhiteEnemyHitHandler();
            white.Add(activePlayer);
            enemyCount = white.Count;

            //BlackPrefabで初期化
            activePlayer = Instantiate(blackPlayerPrefab,startBlackPlayerPos[turn % 10],blackPlayerPrefab.transform.rotation);
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
        {
            if (enemy == null)
                continue;
            enemy.SetActive(true);
        }
        foreach (GameObject enemy in white)
        {
            if (enemy == null)
                continue;
            enemy.SetActive(true);
        }
        activePlayer.SetActive(true);
    }

    private void ChangeStage()
    {
        cameraEffect.Play();
        Invoke("DestroyCharacter", + StageManager.EffectWaitInterval / 2);
    }
    private void DestroyCharacter()
    {
        if (currentState == BLACK)
        {
            foreach (GameObject enemy in black)
                Destroy(enemy);
            black.Clear();
        }
        else
        {
            foreach (GameObject enemy in white)
                Destroy(enemy);
            white.Clear();
        }
    }

    private void ActivePlayerInput()
    {
        activePlayer.AddComponent<PlayerController>();
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1f);

        Destroy(activePlayer.GetComponent<PlayerController>());
        turn++;
        OnNextGame();

        yield return new WaitForSeconds(1f);

        ResetGame();

        if (stageManager.GetTurnAfterReset())
            DestroyCharacter();
        yield return null;

        ShowAllCharacter();

        if (stageManager.GetNeedToReset())
        {
            ChangeStage();
            yield return new WaitForSeconds(StageManager.EffectWaitInterval);
        }

        yield return new WaitForSeconds(0.5f);
        ActivePlayerInput();
    }

    private void OnDestroy()
    {
        OnNextGame = null;
    }

    //IEnumerator CallGameEnd()
    //{
    //    Debug.Log("Call end");
    //    yield return new WaitForSeconds(1);

    //    SceneController.Instance.Change(Scene.Result);
    //}
}
