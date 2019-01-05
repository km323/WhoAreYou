using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    private float period;

    [SerializeField]
    private Vector2 initialVelocityRangeX;
    [SerializeField]
    private Vector2 initialVelocityRangeY;

    private Vector3 velocity;
    private Vector3 position;

    private GameObject target;

    private void Awake()
    {
        position = transform.position;
        Destroy(gameObject, 5f);
        velocity.x = Random.Range(initialVelocityRangeX.x, initialVelocityRangeX.y);
        velocity.y = Random.Range(initialVelocityRangeY.x, initialVelocityRangeY.y);
    }

    public void initMissile(GameObject target, int initialVelocityDirectionX)
    {
        this.target = target;
        velocity.x *= initialVelocityDirectionX;
        Debug.Log(velocity.x + "missile");
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
        else if (target.activeSelf == true)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        position += velocity * Time.deltaTime;
        transform.position = position;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion targetAngle = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = targetAngle;
    }
}
