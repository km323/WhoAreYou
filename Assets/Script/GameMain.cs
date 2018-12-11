using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    const int BLACK = 1;
    const int WHITE = -1;

    private int currentState;

    public int GetCurrentState { get { return currentState; } }

    private List<Player> players;

    private List<Player> black;
    private List<Player> white;

    bool clearStage = false;

    [SerializeField]
    private GameObject blackObject;
    [SerializeField]
    private GameObject whiteObject;

    private GameObject activePlayer;



    // Use this for initialization
    void Start()
    { 
        black = new List<Player>();
        white = new List<Player>();

        currentState = BLACK;

        activePlayer = Instantiate(blackObject);
        activePlayer.GetComponent<Player>().startPosition = new Vector3(0, -3);
        activePlayer.AddComponent<PlayerController>();
    }

    private void StartGame()
    {

    }
    
    private void ResetGame()
    {
        Debug.Log(black[0]);

        if (currentState == BLACK)
            activePlayer = Instantiate(blackObject);
        else
            activePlayer = Instantiate(whiteObject);

        activePlayer.AddComponent<PlayerController>();


        GameObject G;

        foreach (Player p in black)
        {
            
            G = Instantiate(blackObject, p.startPosition, blackObject.transform.rotation);
            Player tmp = G.GetComponent<Player>();
            tmp = p;
        }

        foreach (Player p in white)
        {
            G = Instantiate(whiteObject, p.startPosition, whiteObject.transform.rotation);
            Player tmp = G.GetComponent<Player>();
            tmp = p;
        }
    }

    private void NextGame()
    {
        if (currentState == BLACK)
            black.Add(activePlayer.GetComponent<Player>().list);
        else
            white.Add(activePlayer.GetComponent<Player>());

        Destroy(activePlayer.gameObject);
        currentState = -currentState;

        GameObject.Find("Main Camera").GetComponent<Camera>().CameraRotate();
    }

    private void GameOver()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (black.Count > 0)
        {
            if (black[0] != null)
                Debug.Log(black[0]);
        }

        if ( Input.GetKeyDown(KeyCode.Q))
        {
            EnemyDestroy();
            NextGame();

            //StartCoroutine("GameSetting");
            ResetGame();
        }
    }

    private void EnemyDestroy()
    {
        
        foreach (Player p in white)
            Destroy(p.gameObject);
        foreach (Player p in black)
        {
            Debug.Log(black[0]);
            Destroy(p.gameObject);
        }

        //if (currentState==BLACK)
        //{
        //    foreach (Player p in white)
        //        Destroy(p.gameObject);
        //}
        //else
        //{
        //    foreach (Player p in black)
        //        Destroy(p.gameObject);
        //}
    }

    IEnumerator GameSetting()
    {
        yield return new WaitForSeconds(1);

        ResetGame();
        
    }
}
