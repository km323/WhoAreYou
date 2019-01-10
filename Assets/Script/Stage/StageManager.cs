using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    private const int stageMax = 5;
    private const int resetTurn = 11;
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

    private bool clearLastStage;
    public bool getClearLastStage()
    {
        return clearLastStage;
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
        clearLastStage = false;
        turnAfterReset = false;
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

        if (!clearLastStage && curStageNum > stageMax)
            clearLastStage = true;

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

    public Sprite GetPreviousBlack()
    {
        return stageDataBase.GetStageList()[previousStage - 1].GetBlack();
    }
    public Sprite GetPreviousWhite()
    {
        return stageDataBase.GetStageList()[previousStage - 1].GetWhite();
    }

    public Sprite GetCurrentBlack()
    {
        return GetStageTable().GetBlack();
    }
    public Sprite GetCurrentWhite()
    {
        return GetStageTable().GetWhite();
    }

    public float GetBgScrollSpeed()
    {
        return stageDataBase.GetStageList()[curStageNum - 1].GetBgScrollSpeed();
    }
    public float GetPressTimeNeed()
    {
        return stageDataBase.GetStageList()[curStageNum - 1].GetPressTimeNeed();
    }
}
