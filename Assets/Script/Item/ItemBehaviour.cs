using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        //if (GameMain.GetCurrentState() == GameMain.BLACK)
        //    itemRenderer.sprite = itemBlack;
        //else
        //    itemRenderer.sprite = itemWhite;

        Vector3 direction = Vector3.Scale(Camera.main.transform.up, transform.up);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
            Mathf.Acos(direction.y) * Mathf.Rad2Deg);
        Debug.Log(transform.eulerAngles);

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f);

        item = GameObject.Find("ItemManager").GetComponent<ItemManager>().GetItem();

        itemIconRenderer.sprite = item.GetIcon();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CenterLine")
            Destroy(gameObject);
    }
}
