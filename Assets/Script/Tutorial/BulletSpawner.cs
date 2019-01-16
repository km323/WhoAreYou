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
        yield return null;
        while (true)
        {
            if (control.GetPlayerInput().TouchTime < control.timeNeedDodge)
                yield return null;
            else
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
