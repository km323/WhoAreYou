using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dodge : MonoBehaviour {
    private Sequence s;
    private PolygonCollider2D polygonCollider;

	// Use this for initialization
	void Start () {
        polygonCollider = GetComponent<PolygonCollider2D>();
        s = DOTween.Sequence();

        s.AppendCallback(() => 
        {
            polygonCollider.enabled = false;
            Time.timeScale = 1f;
            GameObject.Find("PostCamera").SetActive(false);
        })
        .Append(
             transform.DORotate(new Vector3(0, 89, 0), 0.3f)
        )
        //.AppendInterval(0.4f)
        .Append(
             transform.DORotate(Vector3.zero, 0.3f)
        )
        .OnComplete(() => { polygonCollider.enabled = true; });
    }
	
	// Update is called once per frame
	public void DodgeAttack() {
        if (!s.IsPlaying())
            s.Restart();
	}
}
