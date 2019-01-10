using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shield : MonoBehaviour {
    private ParticleSystem effect;
    private GameMain gameMain;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        effect = GetComponentInChildren<ParticleSystem>();
        gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
        UpdatePosition();
    }

    // Use this for initialization
    void Start () {
        transform.DOScale(Vector3.one, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = gameMain.GetActivePlayer().transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet")
            return;

        Destroy(collision.gameObject);
        StartCoroutine("Vanish");
    }

    IEnumerator Vanish()
    {
        SoundManager.Instance.PlaySe(SE.ShieldBreak);
        effect.Play();
        transform.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
