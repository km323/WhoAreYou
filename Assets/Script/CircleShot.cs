using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShot : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float deadDelayTime;

    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;

    [SerializeField]
    private float width;
    private float time;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        Destroy(gameObject, deadDelayTime);

        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Mathf.Cos(time*15) * width, transform.position.y);
    }
}
