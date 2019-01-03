using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextButton : MonoBehaviour {
    [SerializeField]
    private Vector2 targetPos;

    private Sequence s;
    private RectTransform rectTransform;
    private Vector2 initPos;

	void Awake () {
        rectTransform = GetComponent<RectTransform>();
        initPos = rectTransform.anchoredPosition;

        s = DOTween.Sequence();

        s.PrependCallback(() => rectTransform.anchoredPosition = initPos)
        .Append(rectTransform.DOAnchorPosY(targetPos.y, 1.5f))
        .SetEase(Ease.InOutQuad);
	}

    private void OnEnable()
    {
        s.Restart();
    }

    private void OnDisable()
    {
        s.Pause();
    }
}
