using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "StageDataBase", menuName = "CreateStageDataBase")]
public class StageDataBase : ScriptableObject
{
    [SerializeField]
    private List<StageTable> stageList = new List<StageTable>();

    public List<StageTable> GetStageList()
    {
        return stageList;
    }
}
