using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollBackground : MonoBehaviour {
    [SerializeField]
    private float speedUpAmount;
    [SerializeField]
    private GameObject pixelWhite;
    [SerializeField]
    private GameObject pixelBlack;

    private StageManager stageManager;
    private Vector3 initPosition;
    private Vector3 direction;
    private bool canScroll;
    private float scrollSpeed;

    private void Awake()
    {
        pixelWhite.SetActive(false);
        pixelBlack.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        initPosition = transform.position;
        StartCoroutine("ResetScroll");
        UpdateDirection();

        GameMain.OnNextGame += () => StartCoroutine("ResetScroll");

        pixelWhite.SetActive(false);
        pixelBlack.SetActive(true);
        scrollSpeed = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        Scroll();
    }

    //回転中はスクロールしない
    IEnumerator ResetScroll()
    {
        canScroll = false;
        ResetPosition();
        ChangePixelEffect();
        
        yield return new WaitForSeconds(1.5f);
        SetScrollSpeed();
        canScroll = true;
    }

    public void ResetPosition()
    {
        transform.DOMoveY(initPosition.y, 1);
    }

    //current state に合うピクセルエフェクトを変える
    private void ChangePixelEffect()
    {
        if(GameMain.GetCurrentState() == GameMain.BLACK)
        {
            pixelWhite.SetActive(false);
            pixelBlack.SetActive(true);
        }
        else
        {
            pixelWhite.SetActive(true);
            pixelBlack.SetActive(false);
        }
    }

    private void SetScrollSpeed()
    {
        if (stageManager.GetNeedToReset())
        {
            if(stageManager.getClearLastStage())
                scrollSpeed += speedUpAmount;
            else
                scrollSpeed = stageManager.GetBgScrollSpeed();
        }
    }

    //背景のスクロール
    private void Scroll()
    {
        if (!canScroll)
            return;

        UpdateDirection();
        transform.position += direction * scrollSpeed * Time.deltaTime;
    }

    //スクロールする方向を決める
    private void UpdateDirection()
    {
        direction = Vector3.Scale(Camera.main.transform.up, -transform.up);
    }

    public void ScrollToBottom()
    {
        UpdateDirection();
        transform.DOMoveY(direction.y * Camera.main.orthographicSize, 1.5f);
    }
}
