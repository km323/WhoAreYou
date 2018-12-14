using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision  : MonoBehaviour {
    public delegate void BulletHitHandler();
    public event BulletHitHandler OnBulletHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //弾を消す
        if(collision.tag == "Bullet")
            Destroy(collision.gameObject);

        if (OnBulletHit != null)
            OnBulletHit();

        //gameObject.SetActive(false);
    }
}
