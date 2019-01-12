using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeGauge : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] gauge;

    protected const float firstPhase = 1 / 3f;
    protected const float secondPhase = 2 / 3f;

    private StageManager stageManager;
    protected float pressedTime;
    protected float pressedTimeNeed;
    private int oldCurrentStage;
    private bool hasGameover;

    protected Gauge oldGauge;

    protected enum Gauge
    {
        None = -1,
        firstPhase,
        secondPhase,
        thirdPhase,
    }

    // Use this for initialization
    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        oldCurrentStage = GameMain.GetCurrentState();
        pressedTimeNeed = stageManager.GetPressTimeNeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMain.GetCurrentState() != oldCurrentStage)
        {
            foreach (GameObject obj in gauge)
            {
                if (GameMain.GetCurrentState() == GameMain.BLACK)
                    obj.GetComponent<Image>().color = stageManager.GetColorBlack();
                else
                    obj.GetComponent<Image>().color = stageManager.GetColorWhite();
            }

            pressedTimeNeed = stageManager.GetPressTimeNeed();
        }

        oldCurrentStage = GameMain.GetCurrentState();

        pressedTime = PlayerController.GetPlayerInput().TouchTime;

        if (FindObjectOfType<GameMain>() != null)
            UpdateMask();
        else if (!hasGameover)
        {
            foreach (GameObject obj in gauge)
                obj.SetActive(false);
            Invoke("ShowAllGauge", 4f);
            hasGameover = true;
        }
    }

    // 3段階 (過ぎてる時間の割合：どのテクスチャ番号）  
    //1/3:2,  2/3:1,  1:0
    protected void UpdateMask()
    {
        if (ReachNeedTime(pressedTimeNeed))
            SetGaugeActive(Gauge.thirdPhase);
        else if (ReachNeedTime(pressedTimeNeed * secondPhase))
            SetGaugeActive(Gauge.secondPhase);
        else if (ReachNeedTime(pressedTimeNeed * firstPhase))
            SetGaugeActive(Gauge.firstPhase);
        else
            SetGaugeActive(Gauge.None);
    }
    protected void SetGaugeActive(Gauge curGauge)
    {
        if (curGauge == oldGauge)
            return;

        switch (curGauge)
        {
            case Gauge.firstPhase:
                gauge[2].SetActive(true);
                break;
            case Gauge.secondPhase:
                gauge[1].SetActive(true);
                break;
            case Gauge.thirdPhase:
                gauge[0].SetActive(true);
                SoundManager.Instance.PlaySe(SE.DodgeGaugeMax);
                break;
            case Gauge.None:
                foreach (GameObject obj in gauge)
                    obj.SetActive(false);
                break;
        }
        oldGauge = curGauge;
    }

    protected bool ReachNeedTime(float needTime)
    {
        if (pressedTime >= needTime)
            return true;
        else
            return false;
    }

    private void ShowAllGauge()
    {
        foreach (GameObject obj in gauge)
            obj.SetActive(true);
    }
}