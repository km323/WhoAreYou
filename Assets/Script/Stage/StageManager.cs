using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    private const int stageMax = 5;
    private const int resetTurn = 10;
    public const float EffectWaitInterval = 2f;

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

    private bool turnAfterReset;
    public bool GetTurnAfterReset()
    {
        return turnAfterReset;
    }

    private GameMain gameMain;
    private int turn;
    private int previousTurn;
    private int randomStage;
    private int previousStage;

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
        turn = gameMain.GetTurn();

        if (turn == previousTurn)
            return;

        needToReset = false;
        turnAfterReset = false;

        if (turn % resetTurn == 0)
        {
            needToReset = true;
            previousStage = curStageNum;

            if (curStageNum <= stageMax)
                curStageNum++;
            if (curStageNum > stageMax)
                randomStage = GetRandomStage();
        }

        if (turn != 1 && turn % resetTurn == 1)
            turnAfterReset = true;

        previousTurn = turn;
    }

    private int GetRandomStage()
    {
        int tmpStage = Random.Range(1, stageMax);

        while (tmpStage == randomStage)
            tmpStage = Random.Range(1, stageMax);

        return tmpStage;
    }

    private StageTable GetStageTable()
    {
        int num = 0;
        if (curStageNum > stageMax)
            num = randomStage;
        else
            num = curStageNum;
        return stageDataBase.GetStageList()[num - 1];
    }

    public Sprite GetPreviousBlackRec()
    {
        return stageDataBase.GetStageList()[previousStage - 1].GetBlackRec();
    }

    public Sprite GetPreviousWhiteRec()
    {
        return stageDataBase.GetStageList()[previousStage - 1].GetWhiteRec();
    }

    public Sprite GetPlayerBlackRec()
    {
        return GetStageTable().GetBlackRec();
    }

    public Sprite GetPlayerWhiteRec()
    {
        return GetStageTable().GetWhiteRec();
    }

    public Sprite GetPlayerBlackPlay()
    {
        return GetStageTable().GetBlackPlay();
    }

    public Sprite GetPlayerWhitePlay()
    {
        return GetStageTable().GetWhitePlay();
    }
}
