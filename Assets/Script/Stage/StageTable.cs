using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

[Serializable]
[CreateAssetMenu(fileName = "StageTable", menuName = "CreateStageTable")]
public class StageTable : ScriptableObject
{
    [SerializeField]
    private Sprite playerBlack;
    [SerializeField]
    private Sprite playerWhite;

    [SerializeField]
    private Color colorBlack;
    [SerializeField]
    private Color colorWhite;

    [SerializeField]
    private float bgScrollSpeed;
    [SerializeField]
    private float pressTimeNeed;

    [SerializeField]
    private BGM bgm;

    public Sprite GetBlack()
    {
        return playerBlack;
    }

    public Sprite GetWhite()
    {
        return playerWhite;
    }

    public Color GetColorBlack()
    {
        return colorBlack;
    }

    public Color GetColorWhite()
    {
        return colorWhite;
    }

    public float GetBgScrollSpeed()
    {
        return bgScrollSpeed;
    }

    public float GetPressTimeNeed()
    {
        return pressTimeNeed;
    }

    public BGM GetBgm()
    {
        return bgm;
    }
}
