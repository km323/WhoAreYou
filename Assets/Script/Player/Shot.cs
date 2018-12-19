using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Vector3 shotOffset;
    [SerializeField]
    private GameObject laserPrefab;

    public void ShotBullet()
    {
        Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
    }
    public void ChargeShotBullet(float time)
    {
        if (time < 0.5f)
            Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
        else
        {
            Instantiate(laserPrefab, transform.position + shotOffset, Quaternion.identity);
        }
    }
}
