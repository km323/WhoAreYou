using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject ItemPrefab;

    [SerializeField]
    private ItemDataBase itemDataBase;

    [SerializeField]
    private int ItemPopProbability;

    [SerializeField]
    private int ItemPopStartNum;

    private GameObject hasItemGameObject;
    private GameObject ItemGameObject;
    private Item item;

    List<Item> items;
    // Use this for initialization
    void Awake () {
        hasItemGameObject = null;
        items = itemDataBase.GetItemLists();



        item = items[0];
    }

    private void Start()
    {
        
        GameMain.OnNextGame += RemoveItem;
    }


    public void SetItem(List<GameObject> friends)
    {
        if (friends.Count <= ItemPopStartNum)
            return;

        if (Random.Range(0, ItemPopProbability) != 0)
            return;

        hasItemGameObject = friends[Random.Range(0, friends.Count)];

        if (hasItemGameObject == null)
            return;
        

        item = items[Random.Range(0, items.Count)];

        hasItemGameObject.GetComponent<PlayerCollision>().OnBulletHit += ActiveItem;
    }

    public Item GetItem()
    {
        if (item == null)
            return null;

        return item;
    }

    private void RemoveItem()
    {
        if (hasItemGameObject == null)
            return;

        hasItemGameObject.GetComponent<PlayerCollision>().OnBulletHit -= ActiveItem;
        hasItemGameObject = null;

        item = null;

        if (ItemGameObject != null)
            Destroy(ItemGameObject);
    }

    private void ActiveItem()
    {
        ItemGameObject = Instantiate(ItemPrefab, hasItemGameObject.transform.position, Quaternion.identity);
    }
    
}
