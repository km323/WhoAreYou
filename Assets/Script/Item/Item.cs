using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{

    public enum KindOfItem
    {
        Attack,
        Defence,
    }

    [SerializeField]
    private string itemName;
    [SerializeField]
    private KindOfItem kindOfItem;
    [SerializeField]
    private Sprite iconBlack;
    [SerializeField]
    private Sprite iconWhite;

    [SerializeField]
    private GameObject itemEffectBlack;
    [SerializeField]
    private GameObject itemEffectWhite;

    public KindOfItem GetKindOfItem()
    {
        return kindOfItem;
    }

    public Sprite GetIcon()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return iconBlack;
        else
            return iconWhite;
    }

    public string GetItemName()
    {
        return itemName;
    }
    public GameObject GetItemEffect()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return itemEffectBlack;
        else
            return itemEffectWhite;
    }
}
