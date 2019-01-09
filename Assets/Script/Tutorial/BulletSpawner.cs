using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;

    private void OnEnable()
    {
        StartCoroutine("Spawn");
    }

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

    }
}
