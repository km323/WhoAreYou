using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer itemRenderer;
    [SerializeField]
    private SpriteRenderer itemIconRenderer;

    [SerializeField]
    private Sprite itemBlack;
    [SerializeField]
    private Sprite itemWhite;

    private Item item;

    void Start()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            itemRenderer.sprite = itemBlack;
        else
            itemRenderer.sprite = itemWhite;

        item = GameObject.Find("ItemManager").GetComponent<ItemManager>().GetItem();

        itemIconRenderer.sprite = item.GetIcon();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CenterLine")
            Destroy(gameObject);
    }
}
