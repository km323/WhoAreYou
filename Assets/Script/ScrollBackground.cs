using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollBackground : MonoBehaviour {
    [SerializeField]
    private float speed;

    private Vector3 initPosition;
    private Vector3 direction;
    private bool canScroll;

	// Use this for initialization
	void Start () {
        initPosition = transform.position;
        canScroll = true;
        UpdateDirection();

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
        transform.DOMoveY(initPosition.y, 1);

        yield return new WaitForSeconds(1);
        canScroll = true;
    }

    private void Scroll()
    {
        if (!canScroll)
            return;

        UpdateDirection();
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void UpdateDirection()
    {
        direction = Vector3.Scale(Camera.main.transform.up, -transform.up);
    }
}
