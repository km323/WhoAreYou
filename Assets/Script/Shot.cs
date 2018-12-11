using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private Vector3 shotOffset;
    [SerializeField]
    private Sprite black;
    [SerializeField]
    private Sprite white;

    public void ShotBullet()
    {
        GameObject shot = Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
        Bullet bullet = shot.GetComponent<Bullet>();
        bullet.Init(Vector2.up, shotSpeed, SetSprite());
    }

    private Sprite SetSprite()
    {
        string curState = "black";
        Sprite sprite;

        if (curState == "black")
            sprite = black;
        else
            sprite = white;

        return sprite;
    }
}
