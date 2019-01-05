using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPrefab;

    [SerializeField]
    private ItemDataBase itemDataBase;

    [SerializeField]
    private int ItemPopProbability;

    [SerializeField]
    private int ItemPopStartNum;

    [SerializeField]
    private Vector3 itemBlackCreatePos;
    [SerializeField]
    private Vector3 itemWhiteCreatePos;


    private GameObject hasItemGameObject;
    private GameObject ItemGameObject;
    private Item item;

    List<Item> items;
    // Use this for initialization
    void Awake()
    {
        hasItemGameObject = null;
        items = itemDataBase.GetItemLists();



        item = items[3];
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

        if (GameObject.FindGameObjectsWithTag("Item") != null)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            {
                Destroy(item);
            }
        }

        item = null;

        if (ItemGameObject != null)
            Destroy(ItemGameObject);

        if (hasItemGameObject != null)
        {
            hasItemGameObject.GetComponent<PlayerCollision>().OnBulletHit -= ActiveItem;
            hasItemGameObject = null;
        }

    }

    private void ActiveItem()
    {
        ItemGameObject = Instantiate(ItemPrefab, hasItemGameObject.transform.position, Quaternion.identity);
    }

    public void CreateItem()
    {
        item = items[Random.Range(0, items.Count)];

        if (GameMain.GetCurrentState() == GameMain.BLACK)
            ItemGameObject = Instantiate(ItemPrefab, itemBlackCreatePos, Quaternion.identity);
        else
            ItemGameObject = Instantiate(ItemPrefab, itemWhiteCreatePos, Quaternion.identity);
    }
}
