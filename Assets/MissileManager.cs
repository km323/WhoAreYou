using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : SingletonMonoBehaviour<MissileManager> {

    [SerializeField]
    private GameObject lockonPrefab;

    List<GameObject> enemys=new List<GameObject>();

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
            enemys.Add(objects[i]);
        }
    }

    public GameObject GetTarget()
    {
        GameObject obj = null;
        int randomNum = 0;

        while (enemys.Count != 0)
        {
            randomNum = Random.Range(0, enemys.Count);
            if (enemys[randomNum] == null)
                enemys.RemoveAt(randomNum);
            else
            {
                obj = enemys[randomNum];
                break;
            }
        }

        if (enemys.Count == 0)
            return null;

        //obj.transform.Find("Lockon").gameObject.GetComponent<Lockon>().SetTargetSprite();
        obj.GetComponentInChildren<Lockon>().SetTargetSprite();
        enemys.RemoveAt(randomNum);

        return obj;
    }
}
