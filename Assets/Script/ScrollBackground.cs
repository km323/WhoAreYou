using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollBackground : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject pixelWhite;
    [SerializeField]
    private GameObject pixelBlack;

    private Vector3 initPosition;
    private Vector3 direction;
    private bool canScroll;

    private void Awake()
    {
        pixelWhite.SetActive(false);
        pixelBlack.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        initPosition = transform.position;
        StartCoroutine("ResetScroll");
        UpdateDirection();
        ChangePixelEffect();

        GameMain.OnNextGame += () => StartCoroutine("ResetScroll");
    }
	
	// Update is called once per frame
	void Update () {
        Scroll();
    }

    //回転中はスクロールしない
    IEnumerator ResetScroll()
    {
        canScroll = false;
        ChangePixelEffect();
        transform.DOMoveY(initPosition.y, 1);

        yield return new WaitForSeconds(1.5f);
        canScroll = true;
    }

    //current state に合うピクセルエフェクトを変える
    private void ChangePixelEffect()
    {
        if(GameMain.GetCurrentState() == GameMain.BLACK)
        {
            pixelWhite.SetActive(true);
            pixelBlack.SetActive(false);
        }
        else
        {
            pixelWhite.SetActive(false);
            pixelBlack.SetActive(true);
        }
    }

    //背景のスクロール
    private void Scroll()
    {
        if (!canScroll)
            return;

        UpdateDirection();
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    //スクロールする方向を決める
    private void UpdateDirection()
    {
        direction = Vector3.Scale(Camera.main.transform.up, -transform.up);
    }
}
