using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lockon : MonoBehaviour {

    [SerializeField]
    private Sprite spriteBlack;
    [SerializeField]
    private Sprite spriteWhite;

    [SerializeField]
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
        {
            GetComponent<SpriteRenderer>().sprite = spriteBlack;
            transform.position += offset;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = spriteWhite;
            transform.position -= offset;
        }

        transform.parent.gameObject.GetComponent<PlayerCollision>().OnBulletHit += LockonDestroy;
        transform.DORotate(new Vector3(0f, 0f,360f), 1, RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(1.15f, 1.15f), 1);
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
