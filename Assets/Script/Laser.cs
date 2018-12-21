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
    
    
    private Vector3 velocity;
    private Rigidbody2D rigid;

    LineRenderer renderer;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<LineRenderer>();

        renderer.enabled = false;
    }

    public void Shot(float width)
    {
        renderer.enabled = true;
        velocity = direction * speed * Time.fixedDeltaTime / 2;
        rigid.AddForce(velocity, ForceMode2D.Impulse);
        Destroy(gameObject, deadDelayTime);

        renderer.SetPosition(0, transform.position);
        renderer.SetPosition(1, transform.position);
        renderer.widthMultiplier = width;
    }

    private void Update()
    {
        renderer.SetPosition(1, transform.position);
    }
}
