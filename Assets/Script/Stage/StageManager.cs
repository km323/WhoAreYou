using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    private const int stageMax = 6;
    private const int resetTurn = 11;

    [SerializeField]
    private StageDataBase stageDataBase;

    private int curStageNum = 1;
    public int GetCurStageNum()
    {
        return curStageNum;
    }

    private bool needToReset;
    public bool GetNeedToReset()
    {
        return needToReset;
    }

    private GameMain gameMain;
    private int turn;

	// Use this for initialization
	void Start () {
        gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
        needToReset = false;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateStage();

    }

    private void UpdateStage()
    {
        needToReset = false;
        if (turn != 0 && turn % resetTurn == 0)
        {
            needToReset = true;
            curStageNum++;
        }
    }

    public Sprite GetPlayerBlackRec()
    {
        StageTable stage = stageDataBase.GetStageList()[curStageNum - 1];
        return stage.GetBlackRec();
    }

    public Sprite GetPlayerWhiteRec()
    {
        StageTable stage = stageDataBase.GetStageList()[curStageNum - 1];
        return stage.GetWhiteRec();
    }

    public Sprite GetPlayerBlackPlay()
    {
        StageTable stage = stageDataBase.GetStageList()[curStageNum - 1];
        return stage.GetBlackPlay();
    }

    public Sprite GetPlayerWhitePlay()
    {
        StageTable stage = stageDataBase.GetStageList()[curStageNum - 1];
        return stage.GetWhitePlay();
    }
}
