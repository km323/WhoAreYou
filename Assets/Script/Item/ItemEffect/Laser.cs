using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float deadDelayTime;
    [SerializeField]
    private float scaleSpeed;

    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;

    private Vector3 scale;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        velocity = direction * speed * Time.fixedDeltaTime / 2;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        Destroy(gameObject, deadDelayTime);
    }

    private void Update()
    {
        transform.localScale += new Vector3(0, Time.fixedDeltaTime*scaleSpeed, 0);
    }
}
