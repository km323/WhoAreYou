using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private Vector3 shotOffset;

    public void ShotBullet()
    {
        GameObject shot = Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
        Bullet bullet = shot.GetComponent<Bullet>();
        bullet.Init(Vector2.up, shotSpeed);
    }
}
