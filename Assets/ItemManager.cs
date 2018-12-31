using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    private GameObject hasItemGameObject;

    [SerializeField]
    private GameObject ItemPrefab;

    [SerializeField]
    private int ItemPopProbability;

    [SerializeField]
    private int ItemPopStartNum;

    // Use this for initialization
    void Start () {
        GameMain.OnNextGame += RemoveItem;
        hasItemGameObject = null;
    }


    public void SetItem(List<GameObject> friends)
    {
        if (friends.Count <= ItemPopStartNum)
            return;

        if (Random.Range(0, ItemPopProbability) != 0)
            return;

        hasItemGameObject=friends[Random.Range(0, friends.Count)];

        if (hasItemGameObject == null)
            return;

        hasItemGameObject.GetComponent<PlayerCollision>().OnBulletHit += ActiveItem;
    }

    private void RemoveItem()
    {
        if (hasItemGameObject == null)
            return;

        hasItemGameObject.GetComponent<PlayerCollision>().OnBulletHit -= ActiveItem;
        hasItemGameObject = null;
    }

    private void ActiveItem()
    {
        Instantiate(ItemPrefab, hasItemGameObject.transform.position, Quaternion.identity);
    }
    
}
