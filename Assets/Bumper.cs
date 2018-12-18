using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	[SerializeField]
    private float power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 vel = (collision.transform.position - transform.position).normalized * power * Time.fixedDeltaTime;
        collision.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vel.y, vel.x)*Mathf.Rad2Deg+90);
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);
    }
}
