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
    private GameObject target;

    private void Start()
    {
        position = transform.position;
        Destroy(gameObject, 5f);

        target = MissileManager.Instance.GetTarget();
    }

    private void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        var acceleration = Vector3.zero;

        var diff = target.transform.position - position;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        //if (acceleration.magnitude > 10f)
        //    acceleration = acceleration.normalized * 10;

        period -= Time.deltaTime;
        if (period > 0f)
        {
            velocity += acceleration * Time.deltaTime;
        }
        else if(target.activeSelf == true)
        {
            target.SetActive(false);
            Destroy(gameObject);
        }
        position += velocity * Time.deltaTime;
        transform.position = position;

    }
}
