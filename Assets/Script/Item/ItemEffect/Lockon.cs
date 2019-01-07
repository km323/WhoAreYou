using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lockon : MonoBehaviour {

    [SerializeField]
    private Sprite spriteBlack;
    [SerializeField]
    private Sprite spriteWhite;

	// Use this for initialization
	void Start () {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            GetComponent<SpriteRenderer>().sprite = spriteBlack;
        else
            GetComponent<SpriteRenderer>().sprite = spriteWhite;

        transform.parent.gameObject.GetComponent<PlayerCollision>().OnBulletHit += LockonDestroy;
        transform.DORotate(new Vector3(0f, 0f,360f), 1, RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(1f, 1f), 1);
	}
    private void LockonDestroy()
    {
        transform.parent.gameObject.GetComponent<PlayerCollision>().OnBulletHit -= LockonDestroy;
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        transform.parent.gameObject.GetComponent<PlayerCollision>().OnBulletHit -= LockonDestroy;
        Destroy(gameObject);
    }
}
