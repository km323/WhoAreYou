using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Vector3 shotOffset;

    public void ShotBullet()
    {
        Instantiate(bulletPrefab, transform.position + shotOffset, Quaternion.identity);
    }
}
