using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour {

    [SerializeField]
    private GameObject chargePrefab;
    [SerializeField]
    private Vector3 shotOffset;
    [SerializeField]
    private float chargeStartTime;

    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private GameObject rockonPrefab;

    private GameObject rockon;

    private float scale;

    private float chargeTime;

    GameObject laser = null;
    GameObject charge = null;



    public void ShotBullet()
    {
        StopCoroutine("Missile");
        Instantiate(missilePrefab, transform.position + shotOffset, Quaternion.identity);
        //Destroy(charge);
        //laser.GetComponent<Laser>().Shot(scale);
        Destroy(rockon);
    }
    public void ChargeBullet()
    {
        rockon = Instantiate(rockonPrefab);
        StartCoroutine("Missile");
        
        //Instantiate(laserPrefab, transform.position + shotOffset, Quaternion.identity);
        
    }

    IEnumerator Missile()
    {
        int layer = 1 << 10;
        while (true)
        {
            RaycastHit2D hit = RaycastAndDraw(transform.position + shotOffset, Vector2.up, 20, layer);
            
            yield return null;
        }
    }

    public RaycastHit2D RaycastAndDraw(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

        //衝突時のRayを画面に表示
        if (hit.collider)
        {
            rockon.SetActive(true);
            rockon.transform.position = hit.collider.gameObject.transform.position;
            Debug.DrawRay(origin, hit.point - origin, Color.yellow);
            //Debug.DrawRay(origin, hit.point - origin, Color.blue, RAY_DISPLAY_TIME, false);
        }
        //非衝突時のRayを画面に表示
        else
        {
            rockon.SetActive(false);
            Debug.DrawRay(origin, direction * maxDistance, Color.green);
            //Debug.DrawRay(origin, direction * maxDistance, Color.green, RAY_DISPLAY_TIME, false);
        }

        return hit;
    }

    IEnumerator Laser()
    {
        chargeTime = 0;
        scale = 1;
        bool first = false;
        

        while (true)
        {
            chargeTime += Time.deltaTime;

            if (chargeTime > chargeStartTime)
            {
                if (first == false)
                {
                    charge=Instantiate(chargePrefab, transform.position + shotOffset, Quaternion.identity);
                    laser=Instantiate(missilePrefab, transform.position + shotOffset, Quaternion.identity);
                    first = true;
                }

                if (scale < 4)
                {
                    scale += Time.deltaTime;
                    charge.transform.localScale = new Vector3(scale, scale, 1);
                    laser.transform.localScale = new Vector3(scale, scale, 1);
                }

                charge.transform.position = transform.position + shotOffset;
                laser.transform.position = transform.position + shotOffset;
            }

            yield return null;
        }
    }
}
