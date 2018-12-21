using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField]
    private Vector3 velocity;
    private Vector3 position;

    [SerializeField]
    private float period;

    [SerializeField]
    private Transform target;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        var acceleration = Vector3.zero;

        acceleration += new Vector3(0, 9.8f, 0);

        var diff = target.position - position;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;

        period -= Time.deltaTime;
        if (period < 0f)
            return;
    }
}
