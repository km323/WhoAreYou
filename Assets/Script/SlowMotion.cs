﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {
    [SerializeField]
    private float timeScale = 0.2f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField]
    private GameObject linePrefab;

    private GameObject postCamera;
    private GameObject activePlayer;
    private int oldCurrentState;
    private StageManager stageManager;
    private BoxCollider2D boxCollider;
    private GameObject bulletNearPlayer;

    // Use this for initialization
    void Start()
    {
        postCamera = GameObject.Find("PostCamera");
        postCamera.SetActive(false);
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
        }

        if (activePlayer == null)
            return;

        if (PlayerController.GetPlayerInput().TouchTime >= stageManager.GetPressTimeNeed())
            boxCollider.enabled = true;
        else
            boxCollider.enabled = false;

        transform.position = activePlayer.transform.position + offset * oldCurrentState;
    }

    private void ResetCollider()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activePlayer == null ||  collision.gameObject.layer == activePlayer.layer)
            return;

        if (collision.tag == "Bullet")
        {
            Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            bulletNearPlayer = collision.transform.GetChild(0).gameObject;
            bulletNearPlayer.AddComponent<GhostSprites>();
            postCamera.SetActive(true);
            Time.timeScale = timeScale;
            Invoke("DelayResetTime", 0.2f);
            SoundManager.Instance.PlaySe(SE.SlowMotion);
            
;        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Time.timeScale = 1f;
            postCamera.SetActive(false);
            foreach (GhostSprites ghost in FindObjectsOfType<GhostSprites>())
                Destroy(ghost);
        }
    }

    private void ChangeActivePlayer()
    {
        activePlayer = GameObject.Find("GameMain").GetComponent<GameMain>().GetActivePlayer();
    }

    private void DelayResetTime()
    {
        Time.timeScale = 1f;
        boxCollider.enabled = false;
        postCamera.SetActive(false);
        foreach (GhostSprites ghost in FindObjectsOfType<GhostSprites>())
            Destroy(ghost);
    }
}
