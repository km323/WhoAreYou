using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnText : MonoBehaviour {

    private GameMain gameMain;
    private int turn;
    private Sequence s;

	// Use this for initialization
	void Start () {
        s = DOTween.Sequence();

        gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
        GameMain.OnNextGame += ShowTurn;

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
        SetAnimation();
    }

    private void ShowTurn()
    {
        turn = gameMain.GetTurn();
        Play();
    }

    private void SetAnimation()
    {
        s.PrependCallback(() => gameObject.SetActive(true))
        .PrependCallback(() => GetComponent<Text>().text = turn.ToString())
        .Append(transform.DOScale(1, 0.2f))
        .AppendInterval(0.6f)
        .Append(transform.DOScale(0, 0.2f))
        .AppendCallback(() => gameObject.SetActive(false));
    }

    private void Play()
    {
        s.Restart();
    }

    private void DelayDisable()
    {
        gameObject.SetActive(false);
    }
}
