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
    // Use this for initialization
    void Start()
    {
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
        }

        if (activePlayer == null)
            return;

        transform.position = activePlayer.transform.position + offset * oldCurrentState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activePlayer == null ||  collision.gameObject.layer == activePlayer.layer)
            return;

        if (collision.tag == "Bullet")
        {
            Time.timeScale = timeScale;
            Invoke("DelayResetTime", 1f);
        }
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
