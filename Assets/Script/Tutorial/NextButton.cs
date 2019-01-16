using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextButton : MonoBehaviour {
    [SerializeField]
    private Vector2 targetPos;

    private float clickScale;
    private Sequence s;
    private RectTransform rectTransform;
    private Vector2 initPos;

	void Awake () {
        rectTransform = GetComponent<RectTransform>();
        initPos = rectTransform.anchoredPosition;
        clickScale = rectTransform.localScale.x;

        s = DOTween.Sequence();

        s.PrependCallback(() => rectTransform.anchoredPosition = initPos)
        .Append(rectTransform.DOAnchorPosX(targetPos.x, 0.5f))
        .SetEase(Ease.InOutQuad);
	}

    private void OnEnable()
    {
        GetComponent<RectTransform>().localScale = new Vector3(clickScale, clickScale, clickScale);
        s.Restart();
    }

    private void OnDisable()
    {
        s.Pause();
    }

    public void OnClick()
    {
        GetComponent<RectTransform>().localScale = new Vector3(clickScale - 0.1f, clickScale - 0.1f, clickScale - 0.1f);
    }
}
