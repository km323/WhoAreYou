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
        target = GameObject.Find("GameObject").transform;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        var acceleration = Vector3.zero;

        var diff = target.position - position;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        //if (acceleration.magnitude > 10f)
        //    acceleration = acceleration.normalized * 10;
        
        period -= Time.deltaTime;
        if (period > 0f)
        {
            velocity += acceleration * Time.deltaTime;
            
        }

        position += velocity * Time.deltaTime;
        transform.position = position;

    }
}
