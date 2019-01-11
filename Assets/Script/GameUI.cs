using System.Collections;
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
    [SerializeField]
    private Color blue;
    [SerializeField]
    private Color orange;

    private const float duration = 0.5f;

    private Sequence startSequence;

    void Start () {
        GameMain.OnNextGame += UpdateUI;

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

        leftUp.DOFade(0, duration);
        rightDown.DOFade(0, duration);

        yield return new WaitForSeconds(0.5f);

        leftUp.color = blue;
        rightDown.color = blue;

        leftUp.DOFade(1, duration);
        rightDown.DOFade(1, duration);
    }
    IEnumerator WhiteUI()
    {
        ChangeColor(boarder, white, 1f);
        ChangeColor(pause, white, 1f);

        leftUp.DOFade(0, duration);
        rightDown.DOFade(0, duration);

        yield return new WaitForSeconds(0.5f);

        leftUp.color = orange;
        rightDown.color = orange;

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
