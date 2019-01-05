using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {
    [SerializeField]
    private float timeScale = 0.2f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 2, 0);

    private GameObject activePlayer;
    private int oldCurrentState;
    private StageManager stageManager;
    private BoxCollider2D boxCollider;
    private int touchTimes = 0; //弾がslow motion 範囲に触れた回数

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        oldCurrentState = GameMain.GetCurrentState();
        ChangeActivePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldCurrentState != GameMain.GetCurrentState())
        {
            oldCurrentState = GameMain.GetCurrentState();
            ChangeActivePlayer();
            touchTimes = 0;
            boxCollider.enabled = true;
        }

        if (activePlayer == null)
            return;

        if (touchTimes > stageManager.GetSlowMotionTimes())
            boxCollider.enabled = false;

        transform.position = activePlayer.transform.position + offset * oldCurrentState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activePlayer == null ||  collision.gameObject.layer == activePlayer.layer)
            return;

        if (collision.tag == "Bullet")
        {
            touchTimes++;
            Time.timeScale = timeScale;
            Invoke("DelayResetTime", 0.15f)
;        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
            Time.timeScale = 1f;
    }

    private void ChangeActivePlayer()
    {
        activePlayer = GameObject.Find("GameMain").GetComponent<GameMain>().GetActivePlayer();
    }

    private void DelayResetTime()
    {
        Time.timeScale = 1f;
    }
}
