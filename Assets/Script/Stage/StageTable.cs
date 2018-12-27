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
    private Sprite playerBlackRec;
    [SerializeField]
    private Sprite playerWhiteRec;

    [SerializeField]
    private Sprite playerBlackPlay;
    [SerializeField]
    private Sprite playerWhitePlay;

    public Sprite GetBlackRec()
    {
        return playerBlackRec;
    }

    public Sprite GetWhiteRec()
    {
        return playerWhiteRec;
    }

    public Sprite GetBlackPlay()
    {
        return playerBlackPlay;
    }

    public Sprite GetWhitePlay()
    {
        return playerWhitePlay;
    }

}
