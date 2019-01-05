using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultBullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float deadDelayTime;

    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;

	void Awake () {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        Destroy(gameObject, deadDelayTime);

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion targetAngle = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = targetAngle;
    }
}
