using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;

	void Awake () {
        rigid = GetComponent<Rigidbody2D>();
    }
	
    public void Init(Vector3 direction, float speed)
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
