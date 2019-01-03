using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
    
    
    [SerializeField]
    private GameObject defaultBulletPrefab;
    [SerializeField]
    private Vector3 shotOffset;

    private GameObject bulletPrefab;

    private void Start()
    {
        SetDefaultBullet();
    }

    public void SetDefaultBullet()
    {
        bulletPrefab = defaultBulletPrefab;
    }

    public void SetBulletPrefab(GameObject bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }
    
    public void ShotBullet()
    {
        Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
    }
}
