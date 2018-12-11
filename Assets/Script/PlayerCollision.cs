using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision  : MonoBehaviour {
    public delegate void OnBulletHit();
    public event OnBulletHit onBulletHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bulletにtagつける？


        //弾を消す
        Destroy(collision.gameObject);
        if (onBulletHit != null)
            onBulletHit();

        gameObject.SetActive(false);
    }
}
