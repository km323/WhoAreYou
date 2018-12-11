using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;
    private SpriteRenderer bulletSprite;

	void Awake () {
        rigid = GetComponent<Rigidbody2D>();
        bulletSprite = GetComponentInChildren<SpriteRenderer>();
    }
	
    public void Init(Vector3 direction, float speed, Sprite sprite)
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        bulletSprite.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
