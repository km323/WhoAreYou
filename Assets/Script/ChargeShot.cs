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
    private GameObject laserPrefab;

    private float scale;

    private float chargeTime;

    GameObject laser = null;
    GameObject charge = null;



    public void ShotBullet()
    {
        //StopCoroutine("Laser");

        //Destroy(charge);
        //laser.GetComponent<Laser>().Shot(scale);

    }
    public void ChargeBullet()
    {
        //StartCoroutine("Laser");
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
                    laser=Instantiate(laserPrefab, transform.position + shotOffset, Quaternion.identity);
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
