﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float deadDelayTime;

    private const float angleOffset = 90;

    private Vector3 velocity;
    private Rigidbody2D rigid;
    private SpriteRenderer bulletSprite;

	void Awake () {
        rigid = GetComponent<Rigidbody2D>();
        bulletSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        Destroy(gameObject, deadDelayTime);
    }
}
