using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private PlayerControlTutorial control;

    private void OnEnable()
    {
        StartCoroutine("Spawn");
    }

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        while (true)
        {
            if (control.GetPlayerInput().TouchTime < control.timeNeedDodge)
                yield return null;
            else
            {
                yield return new WaitForSeconds(2f);
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);   
            }
        }
    }
}
