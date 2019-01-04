using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : SingletonMonoBehaviour<MissileManager>
{

    [SerializeField]
    private GameObject lockonPrefab;

    [SerializeField]
    private GameObject missileBlackPrefab;
    [SerializeField]
    private GameObject missileWhitePrefab;

    // Use this for initialization
    void Start()
    {
        GameObject[] objects;
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            objects = GameObject.FindGameObjectsWithTag("PlayerWhite");
        else
            objects = GameObject.FindGameObjectsWithTag("PlayerBlack");

        for (int i = 0; i < objects.Length; i++)
        {
            Instantiate(lockonPrefab, objects[i].transform);
        }
    }

    private void Update()
    {
        if (PlayerController.GetPlayerInput().SameTimeTap)
            StartCoroutine("Blastoff");
    }

    IEnumerator Blastoff()
    {
        GameObject[] enemys;
        GameObject missilePrefab;
        GameObject player = GameObject.Find("GameMain").GetComponent<GameMain>().GetActivePlayer();
        int directionX = 1;

        if (GameMain.GetCurrentState() == GameMain.BLACK)
        {
            missilePrefab = missileBlackPrefab;
            enemys = GameObject.FindGameObjectsWithTag("PlayerWhite");
        }
        else
        {
            missilePrefab = missileWhitePrefab;
            enemys = GameObject.FindGameObjectsWithTag("PlayerBlack");
        }

        foreach (GameObject enemy in enemys)
        {
            GameObject missile = Instantiate(missilePrefab, player.transform);
            missile.GetComponent<Missile>().initMissile(enemy, directionX);
            Debug.Log(directionX + "manager");
            directionX *= -1;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
