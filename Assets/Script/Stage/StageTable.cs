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
    private float bgScrollSpeed;
    [SerializeField]
    private float pressTimeNeed;

    public Sprite GetBlack()
    {
        return playerBlack;
    }

    public Sprite GetWhite()
    {
        return playerWhite;
    }

    public float GetBgScrollSpeed()
    {
        return bgScrollSpeed;
    }

    public float GetPressTimeNeed()
    {
        return pressTimeNeed;
    }
}
