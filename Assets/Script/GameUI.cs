﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private GameMain gameMain;

    [SerializeField]
    private Transform boarderObj;
    [SerializeField]
    private RectTransform pauseObject;

    [SerializeField]
    private Image boarder;
    [SerializeField]
    private Image pause;
    [SerializeField]
    private Image leftUp;
    [SerializeField]
    private Image rightDown;

    [SerializeField]
    private Vector2 pauseTargetPos;
    [SerializeField]
    private Vector2 leftTargetPos;
    [SerializeField]
    private Vector2 rightTargetPos;

    [SerializeField]
    private Color black;
    [SerializeField]
    private Color white;

    private const float duration = 0.5f;

    private StageManager stageManager;
    private Sequence startSequence;

    void Start () {
        GameMain.OnNextGame += UpdateUI;
        stageManager = FindObjectOfType<StageManager>();

        startSequence = DOTween.Sequence();
        SetStartSequence();
    }

	public void DisablePause()
    {
        pauseObject.DOAnchorPosX(pauseObject.anchoredPosition.x + 300f, 0.5f);
    }

    private void SetStartSequence()
    {
        startSequence
            .Append(boarderObj.DOScale(1, duration))
            .Join(pauseObject.DOAnchorPos(pauseTargetPos, duration))
            .Join(leftUp.rectTransform.DOAnchorPos(leftTargetPos, duration))
            .Join(rightDown.rectTransform.DOAnchorPos(rightTargetPos, duration));

        startSequence.Play();
    }

    private void UpdateUI()
    {
        if (gameMain.GetTurn() % 2 == 0)
            StartCoroutine("BlackUI");
        else
            StartCoroutine("WhiteUI");
    }

    IEnumerator BlackUI()
    {
        ChangeColor(boarder, black, 1f);
        ChangeColor(pause, black, 1f);

        ChangeColor(leftUp, white, 1f);
        ChangeColor(rightDown, white, 1f);

        leftUp.DOFade(0, duration);
        rightDown.DOFade(0, duration);

        yield return new WaitForSeconds(0.5f);

        if (stageManager != null)
        {
            leftUp.color = white;
            rightDown.color = white;

            //leftUp.color = stageManager.GetColorBlack();
            //rightDown.color = stageManager.GetColorBlack();
        }

        leftUp.DOFade(1, duration);
        rightDown.DOFade(1, duration);
        yield return null;
    }
    IEnumerator WhiteUI()
    {
        ChangeColor(boarder, white, 1f);
        ChangeColor(pause, white, 1f);

        //ChangeColor(leftUp, black, 1f);
        //ChangeColor(rightDown, black, 1f);

        //yield return null;

        leftUp.DOFade(0, duration);
        rightDown.DOFade(0, duration);

        yield return new WaitForSeconds(0.5f);

        if (stageManager != null)
        {
            leftUp.color = black;
            rightDown.color = black;

            //leftUp.color = stageManager.GetColorWhite();
            //rightDown.color = stageManager.GetColorWhite();
        }

        leftUp.DOFade(1, duration);
        rightDown.DOFade(1, duration);
    }

    private void ChangeColor(Image target, Color color, float duration)
    {
        DOTween.To(
            () => target.color,
            c => target.color = c,
            color,
            duration
        );
    }
}
