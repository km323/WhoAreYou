using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision  : MonoBehaviour {
    public delegate void BulletHitHandler();
    public BulletHitHandler OnBulletHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SlowMotion")
            return;
      
        if (collision.tag == "Item")
            return;

        //弾を消す
        if(collision.tag == "Bullet")
            Destroy(collision.gameObject);
        if (OnBulletHit != null)
            OnBulletHit();

        SoundManager.Instance.PlaySe(SE.Damage);

        //gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        OnBulletHit = null;
    }
}
